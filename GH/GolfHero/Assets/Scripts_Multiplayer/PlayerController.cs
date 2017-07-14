using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private CameraController cameraController;
    private float power;

    private PlayerManager playerManager;

    private bool shotLock;

    // Use this for initialization
    private void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();

        // set-up on the specific player's device
        if (isLocalPlayer)
        {
            this.cameraController = Camera.main.GetComponent<CameraController>();
            this.power = 20;
        }

    }

    private void Update()
    {
        // owning player's inputs
        if (isLocalPlayer)
        {
            if (playerManager.activeState)
            {
                if (Input.GetButton("Fire1"))
                {
                    Vector3 rawDirection = Camera.main.transform.forward;

                    // project force onto plane (WIP)
                    Vector3 planeNorm = new Vector3(0, 1, 0); //Use the norm of the x,z plane
                    Vector3 direction = Vector3.ProjectOnPlane(rawDirection, planeNorm).normalized;

                    // Send a request to shoot a ball with the given direction and power
                    playerManager.CmdShootBall(direction, power);
                }
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

            if (Input.GetKeyDown("s"))
            {
                playerManager.CmdStopBall();
            }

            if (Input.GetKeyDown("r"))
            {
                playerManager.CmdResetBall();
            }
        }
    }
}
