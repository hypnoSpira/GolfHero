using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallBehaviour : MonoBehaviour {
    
    private enum ballState {
        staionary,
        moving
    }

    private Camera cam;
    private static Rigidbody rb;
	public float power;
    public static Vector3 startPos;
	public GameObject arrow;
	private Renderer[] arrowRend;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        startPos = rb.transform.position;
		arrowRend = arrow.GetComponentsInChildren<Renderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity == new Vector3(0, 0, 0) && Input.GetButton("Fire1")) {
            Vector3 force = cam.transform.forward;
            /*Possibly account for ball being on slope */
            //Vector3 planeNorm = rb.getPlane();
            Vector3 planeNorm = new Vector3(0,1,0); //Use the norm of the x,z plane
            //Debug.Log(force);
            force = Vector3.ProjectOnPlane(force, planeNorm).normalized; //Project onto a flat surface as we dont care about camera height
            Debug.Log(force);
            rb.AddForce(force * power * power);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
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
        }

        if (Input.GetKeyDown("s")) {
            stopBall();
        }

        if (Input.GetKeyDown("r")) {
            stopBall();
            resetBall();
        }

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

        //Power meter stuff
        //while (Input.GetMouseButtonDown(0)) {
        //    Mathf.Sin(2);
        //}
    }

    public static void stopBall() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public static void resetBall() {
        rb.transform.position = startPos;
    }
}
