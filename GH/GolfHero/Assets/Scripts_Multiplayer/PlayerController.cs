using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private CameraController cameraController;

    private PlayerManager playerManager;

    private bool shotLock;
    public int shotMode;

    private Vector3 direction;
    private static float power = 1f;
    private static float maxPower = 36f;
	//public static int timer = 0;
    private bool increase = true;
    private bool canShoot = false;
    private float time;
    private float wait = .08f;

    public GameObject arrow;
    private Renderer arrowRend;
    private Color color;

	public AudioClip lowHit;
	public AudioClip midHit;
	public AudioClip hiHit;
	private AudioSource source;

    // Use this for initialization
    private void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
		source = GetComponent<AudioSource> ();

        // set-up on the specific player's device
        if (isLocalPlayer)
        {
            this.cameraController = Camera.main.GetComponent<CameraController>();
            Cursor.lockState = CursorLockMode.Locked;
            this.shotLock = false;
            this.shotMode = 1;
        }

    }

    private void Update() {
        // owning player's inputs
        if (isLocalPlayer) {
            //if (timer == 0)
            //{
            //    playerManager.Activate();
            //}
            //else
            //{
            //    playerManager.Deactivate();
            //    timer -= 1;
            //}
            if (ChatController.instance.textMode)
                return;

            if (playerManager.activeState && !shotLock) {
                color.r = power/maxPower;
				color.g = power/maxPower;
				color.b = power/maxPower;
                //arrowRend.material.color = color;
                cameraController.SetArrowIntensity(color);

                if (shotMode == 0)
                {
                    if (!canShoot && Input.GetKeyUp("mouse 0"))
                    {
                        direction = Camera.main.transform.forward;
                        canShoot = true;
                        BallCamController.Disabled(false);
                    }
                    else if (canShoot && Input.GetKeyUp("mouse 0"))
                    {
                        canShoot = false;
                        BallCamController.Disabled(false);
                        // shot++;
                        // calcWind = true;
                        /*Possibly account for ball being on slope */
                        //Vector3 planeNorm = rb.getPlane();
                        Vector3 planeNorm = new Vector3(0, 1, 0); //Use the norm of the x,z plane
                        direction = Vector3.ProjectOnPlane(direction, planeNorm).normalized;
                        // Debug.Log(force);
                        // rb.AddForce(force * power * power + windDir * windSpd[2] * windSpd[2]);
                        // power = 1f;
                        playerManager.CmdShootBall(direction, power + 9.5f);
                    }
                    else if (canShoot && Input.GetKeyUp("mouse 1"))
                    {
                        canShoot = false;
                        BallCamController.Disabled(false);
                        power = 1f;
                    }
                }
                else
                {
                    if (Input.GetKeyDown("mouse 0"))
                    {
                        canShoot = true;
                        direction = Camera.main.transform.forward;
                        Vector3 planeNorm = new Vector3(0, 1, 0);
                        direction = Vector3.ProjectOnPlane(direction, planeNorm).normalized;
                    }
                    if (Input.GetKeyDown("mouse 1"))
                    {
                        canShoot = false;
                        power = 1f;
                    }
                    if (canShoot && Input.GetKeyUp("mouse 0"))
                    {
                        canShoot = false;
                        playerManager.CmdShootBall(direction, power + 9.5f);
                        if (power > 30)
                        {
                            source.PlayOneShot(hiHit);
                        }
                        else if (power > 20)
                        {
                            source.PlayOneShot(midHit);
                        }
                        else
                        {
                            source.PlayOneShot(lowHit);
                        }
                        //timer = 3 * (int)power; 
                        power = 1f;
                    }
                }

                

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Space");
                    if (Cursor.lockState == CursorLockMode.Locked)
                        Cursor.lockState = CursorLockMode.None;
                    else
                        Cursor.lockState = CursorLockMode.Locked;
                }
            }

            if (Input.GetKeyDown("s")) {
                playerManager.CmdStopBall();
            }

            if (Input.GetKeyDown("r")) {
                playerManager.CmdResetBall();
            }
        }
    }

    private void LateUpdate()
    {
        if (cameraController == null)
            return;

        if (canShoot)
            cameraController.LockArrow();
        else
            cameraController.UnlockArrow();

        if (CameraController.instance.cameraLock)
            shotLock = true;
        else
            shotLock = false;
    }

    private void FixedUpdate() {
        if (isLocalPlayer) {
            if (canShoot) {
                if (increase) {
                    if (power > maxPower) {
                        if (time >= 0) {
                            power = maxPower;
                            time -= Time.fixedUnscaledDeltaTime;
                            return;
                        } else {
                            increase = false;
                            time = wait;
                            return;
                        }
                    }
                    power += .67f;
                } else {
                    if (power < 1f) {
                        if (time >= 0) {
                            power = 1f;
                            time -= Time.fixedUnscaledDeltaTime;
                            return;
                        } else {
                            increase = true;
                            time = wait;
                            return;
                        }
                    }
                    power -= .67f;
                }
            }
        }
    }
}
