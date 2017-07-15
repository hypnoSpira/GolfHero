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
    private bool increase = true;
    private bool canShoot = false;
    private float time;
    private float wait = .08f;
    private bool camLock = false;

    public GameObject arrow;
    private Renderer arrowRend;
    private Color color;

    // Use this for initialization
    private void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();

        // set-up on the specific player's device
        if (isLocalPlayer)
        {
            this.cameraController = Camera.main.GetComponent<CameraController>();
            //Color color = arrowRend.material.color;
        }

    }

    private void Update() {
        // owning player's inputs
        if (isLocalPlayer) {
            if (playerManager.activeState) {
                color.r = 0.04f * power;
                //arrowRend.material.color = color;

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
                    power = 1f;
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("Space");
                    camLock = !camLock;
                    BallCamController.Disabled(!camLock);
                }
            }

            if (Input.GetKeyDown("s")) {
                playerManager.CmdStopBall();
            }

            if (Input.GetKeyDown("r")) {
                playerManager.CmdResetBall();
            }

            /* Uncomment when arrow is added
            if (canShoot) {
                arrowRend.enabled = true;
            } else {
                arrowRend.enabled = false;
            }*/
        }
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

    // delete this and the part in GameManager when arrow is added
    public static string powerText() {
        return "\nCurrent Power: " + power + "\nMax Power: " + maxPower;
    }
}
