using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBallBehaviour : MonoBehaviour {
    private Camera cam;
    private Rigidbody rb;
    private float power = 20;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 force = cam.transform.forward;
            /*Possibly account for ball being on slope */
            //Vector3 planeNorm = rb.getPlane();
            Vector3 planeNorm = new Vector3(0,1,0); //Use the norm of the x,z plane
            //Debug.Log(force);
            force = Vector3.ProjectOnPlane(force, planeNorm).normalized; //Project onto a flat surface as we dont care about camera height
            Debug.Log(force);
            rb.AddForce(force * power * power);
        }
        //while (Input.GetMouseButtonDown(0)) {
        //    Mathf.Sin(2);
        //}
	}
}
