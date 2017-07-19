using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    // the ball this player owns
    [SyncVar(hook = "OnChangeBall")]
    public GameObject ball;

    // whether this player scored or not on current level
    [SyncVar]
    public bool scored;

    // true = this player can request shot inputs from the server
    [SyncVar(hook = "OnChangeActiveState")]
    public bool activeState;

    // # of strokes taken by this player
    [SyncVar]
    public int strokes;

    // reference to camera controller
    private CameraController cameraController;

    //reference to ball's rigid body for detecting speed thresholds on server-side
    private Rigidbody ballBody;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        // server-side setup for this player
		if (isServer)
        {
            this.strokes = 0;
            this.scored = false;
        }

        if (isClient)
            this.cameraController = Camera.main.GetComponent<CameraController>();

        if (isLocalPlayer)
            CmdGetBall();
    }
	
	// Update is called once per frame
	private void Update () {
        // server sets active state (whether the associated player can do anything), currently just allows movement if no already moving
//        if (isServer && ballBody != null)
//        {
//            if (ballBody.velocity.magnitude < 0.5)
//            {
//                Activate();
//            }
//            else
//            {
//                Deactivate();
//            }
//        }

    }


    public void Activate()
    {
        if (this.activeState == false)
            this.activeState = true;
    }

    public void Deactivate()
    {
        if (this.activeState == true)
            this.activeState = false;
    }


    //client-to-server commands

    // request a new ball
    [Command]
    public void CmdGetBall()
    {
        this.ball = BallsManager.instance.GetBall();
    }

    [Command]
    public void CmdShootBall(Vector3 direction, float power)
    {
        if (activeState)
        {
            power = Mathf.Clamp(power, 1.0f, 36.0f);

            ballBody.AddForce(direction * power * power + WindManager.instance.getWind());
            this.strokes = this.strokes + 1;
        }
    }

    // debug commands

    // request to stop ball
    [Command]
    public void CmdStopBall()
    {
        if (ballBody != null)
        {
            ballBody.velocity = Vector3.zero;
            ballBody.angularVelocity = Vector3.zero;
        }
    }

    // request to reset ball to origin
    [Command]
    public void CmdResetBall()
    {
        BallsManager.instance.ResetBall(ballBody);
    }

    // syncvar updates
    public void OnChangeBall(GameObject ball)
    {

        if (isClient)
            this.ball = ball;

        if (isLocalPlayer && cameraController != null)
        {
            if (ball != null)
            {
                this.cameraController.SetTarget(ball.transform);
            }
            else
            {
                this.cameraController.SetTarget(null);
            }
        }

        if (isServer && (ball != null))
            ball.GetComponent<BallManager>().setPlayerManager(this);

        if (ball != null)
            this.ballBody = ball.GetComponent<Rigidbody>();
        else
            this.ballBody = null;
    }

    public void OnChangeActiveState(bool activeState)
    {
        this.activeState = activeState;

        if (isLocalPlayer && cameraController)
        {
            if (activeState && (ball != null))
            {
                cameraController.SetTarget(ball.transform);
                cameraController.ShowArrow();
            }
            else
                cameraController.HideArrow();
        }
    }
}
