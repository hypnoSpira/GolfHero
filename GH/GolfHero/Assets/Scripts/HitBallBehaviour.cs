using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallBehaviour : MonoBehaviour {
    
    private enum ballState {
        staionary,
        moving
    }

    private Camera cam;
    private Rigidbody rb;
    private float power = 40;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown("s")) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        //while (Input.GetMouseButtonDown(0)) {
        //    Mathf.Sin(2);
        //}
	}
}
