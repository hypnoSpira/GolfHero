using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBallBehaviour : MonoBehaviour {

    private class Position {
        public Vector3 pos;
        public Position prev;

        public Position(Vector3 pos, Position prev) {
            this.pos = pos;
            this.prev = prev;
        }
    }

    private Position curr;
    private Camera cam;
    private static Rigidbody rb;
	public float power;
    public static Vector3 startPos;
    public Vector3 force;
    public GameObject arrow;
	private Renderer[] arrowRend;
    private int stage = 0;
    public int[] windSpd = { 0, 0, 0 };
    public static Vector3 windDir;
    private static bool pause = false;
    public static bool calcWind = true;
    private bool shoot = false;
    private Component windTxt;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        startPos = rb.transform.position;
		arrowRend = arrow.GetComponentsInChildren<Renderer> ();
        curr = new Position(startPos, null);
        checkStage();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update() {
        if (pause) {
            return;
        }

        if (calcWind && rb.velocity == Vector3.zero) {
            windDir = new Vector3(UnityEngine.Random.Range(-1f, 1.1f), 0, UnityEngine.Random.Range(-1f, 1.1f));
            windSpd[2] = UnityEngine.Random.Range(windSpd[0], windSpd[1] + 1);
            //((Text)windTxt).text = "Wind Speed: " + windSpd[2] +"\nWind Direction:" + windDir;
            WindText.setText("Wind Speed: " + windSpd[2] + "km/h\nWind Direction: " + windDir);
            calcWind = false;
        }

        if (!shoot && rb.velocity == Vector3.zero && Input.GetKeyUp("mouse 0")) {
            //cam.enabled = false;
            force = cam.transform.forward;
            shoot = true;
            BallCamController.Disabled(true);
        } else if (shoot && Input.GetKeyUp("mouse 0")) {
            shoot = false;
            BallCamController.Disabled(false);
            /*Possibly account for ball being on slope */
            //Vector3 planeNorm = rb.getPlane();
            Vector3 planeNorm = new Vector3(0, 1, 0); //Use the norm of the x,z plane
            force = Vector3.ProjectOnPlane(force, planeNorm).normalized; //Project onto a flat surface as we dont care about camera height
            Debug.Log(force);
            rb.AddForce(force * power * power + windDir * windSpd[2] * windSpd[2]);
            //cam.enabled = true;
            calcWind = true;
        } else if (shoot && Input.GetKeyUp("mouse 1")) {
            //cam.enabled = true;
            shoot = false;
            BallCamController.Disabled(false);
        }

        if (Input.GetKeyDown("s")) {
            stopBall();
        }
        if (Input.GetKeyDown("r")) {
            stopBall();
            resetBall();
        }

        /*if(Input.GetKeyDown(KeyCode.Alpha1)){
            power = 10;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            power = 20;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            power = 30;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            power = 40;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            power = 50;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            power = 60;
        }*/

        // Hide arrow when ball is in motion
        if (rb.velocity == Vector3.zero) {
			foreach (Renderer rend in arrowRend) {
				rend.enabled = true;
			}

		} else {
			foreach (Renderer rend in arrowRend) {
				rend.enabled = false;
			}
		}
    }

    public static void Pause() {
        pause = true;
    }

    public static void Resume() {
        pause = false;
    }

    public static void stopBall() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public static void resetBall() {
        rb.transform.position = startPos;
    }

    private void checkStage()
    {
        switch (stage)
        {
            case 0:
                windSpd[0] = Stage0.windSpdMin;
                windSpd[1] = Stage0.windSpdMax;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            default:
                break;
        }
    }
}
