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
            Debug.Log(force);
            rb.AddForce(force * power * power);
        }
        //while (Input.GetMouseButtonDown(0)) {
        //    Mathf.Sin(2);
        //}
	}
}
