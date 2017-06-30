using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public ArrowController arrowController;
    public BallSpawner ballSpawner;

    [SyncVar(hook = "OnChangeBall")]
    public GameObject ball;

    [SyncVar(hook = "OnChangeActiveState")]
    public bool activeState;

    private Camera cam;
    private BallCamController ballCamController;
    private Rigidbody ballRigidbody;
    private float power;

    // Use this for initialization
    private void Start()
    {
        // server-side set-up for this player
        if (isServer)
        {
            getBallSpawner();
        }

        // set-up on the specific player's device
        if (isLocalPlayer)
        {
            this.cam = Camera.main;
            this.ballCamController = cam.GetComponent<BallCamController>();
            this.arrowController = cam.GetComponent<ArrowController>();
            this.power = 20;
            CmdGetBall();
        }

    }

    private void Update()
    {
        // server sets active state (whether the associated player can do anything), currently just allows movement if no already moving
        if (isServer && ball != null)
        {
            if (ballRigidbody.velocity == Vector3.zero)
            {
                activeState = true;
            } else
            {
                activeState = false;
            }
        }
 
        // owning player's inputs
        if (isLocalPlayer && ball != null)
        {
            if (activeState == true)
            {
                if (Input.GetButton("Fire1"))
                {
                    CmdShootBall(cam.transform.forward, power);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    power = 10;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    power = 20;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    power = 30;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    power = 40;
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    power = 50;
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    power = 60;
                }
            }

            if (Input.GetKeyDown("s"))
            {
                CmdStopBall();
            }

            if (Input.GetKeyDown("r"))
            {
                CmdStopBall();
                CmdResetBall();
            }
        }
    }

    public void Activate()
    {
        this.activeState = true;
    }
    public void Deactivate()
    {
        this.activeState = false;
    }

    private void getBallSpawner()
    {
        if (ballSpawner == null)
        {
            this.ballSpawner = GameObject.Find("Ball Spawner").GetComponent<BallSpawner>();
        }
    }

    // client-to-server commands

    // request a new ball
    [Command]
    public void CmdGetBall()
    {
        getBallSpawner();
        this.ball = ballSpawner.getBall();
    }

    // request a shot
    [Command]
    public void CmdShootBall(Vector3 force, float power)
    {
        if (activeState)
        {
            // adjust power limits
            power = Mathf.Clamp(power, 1.0f, 100.0f);

            // project force onto plane
            Vector3 planeNorm = new Vector3(0, 1, 0); //Use the norm of the x,z plane
            //Debug.Log(force);
            force = Vector3.ProjectOnPlane(force, planeNorm).normalized;

            ballRigidbody.AddForce(force * power * power);
            activeState = false;
        }
    }

    //debug commands

    // request to stop ball
    [Command]
    public void CmdStopBall()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
    }
    
    // request to reset ball to origin
    [Command]
    public void CmdResetBall()
    {
        ballRigidbody.transform.position = Vector3.zero;
    }

    // syncvar updates
    public void OnChangeBall(GameObject ball)
    {
        if (isClient)
            this.ball = ball;

        if (isLocalPlayer)
        {
            if (ball != null)
            {
                this.ballCamController.target = ball.transform;
                this.arrowController.ballTransform = ball.transform;
                this.ballRigidbody = ball.GetComponent<Rigidbody>();
            }
            else
            {
                this.ballCamController.target = null;
                this.arrowController.ballTransform = null;
                this.ballRigidbody = null;
            }
        }

        // test
        if (isServer)
        {
            if (ball != null)
            {
                this.ballRigidbody = ball.GetComponent<Rigidbody>();
            }
            else
            {
                this.ballRigidbody = null;
            }
            Activate();
        }
    }

    public void OnChangeActiveState(bool activeState)
    {
        if (isClient)
            this.activeState = activeState;

        if (isLocalPlayer && arrowController)
        {
            if (activeState)
            {
                arrowController.ShowArrow();
            }
            else
                arrowController.HideArrow();
        }
    }
}
