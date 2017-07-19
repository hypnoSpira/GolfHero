using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private CameraController cameraController;

    private PlayerManager playerManager;

    private bool shotLock;

    private Vector3 direction;
    private static float power = 1f;
    private static float maxPower = 36f;
	public static int timer = 0;
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
        }

    }

    private void Update() {
        // owning player's inputs
        if (isLocalPlayer) {
			if (timer == 0) {
				playerManager.Activate ();
			} else {
				playerManager.Deactivate ();
				timer -= 1;
			}
            if (playerManager.activeState && !shotLock) {
                color.r = power/maxPower;
				color.g = power/maxPower;
				color.b = power/maxPower;
                //arrowRend.material.color = color;
                cameraController.SetArrowIntensity(color);
                if (Input.GetKeyDown("mouse 0")) {
                    canShoot = true;
                    direction = Camera.main.transform.forward;
                    Vector3 planeNorm = new Vector3(0, 1, 0);
                    direction = Vector3.ProjectOnPlane(direction, planeNorm).normalized;
                }
                if (Input.GetKeyDown("mouse 1")) {
                    canShoot = false;
                    power = 1f;
                }
                if (canShoot && Input.GetKeyUp("mouse 0")) {
                    canShoot = false;
                    playerManager.CmdShootBall(direction, power);
					if (power > 30) {
						source.PlayOneShot (hiHit);
					} else if (power > 20) {
						source.PlayOneShot (midHit);
					} else {
						source.PlayOneShot (lowHit);
					}
					timer = 5 * (int)power; 
                    power = 1f;
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
