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
    private float power = 20;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity == new Vector3(0, 0, 0) && Input.GetMouseButtonDown(0)) {
            Vector3 direction = cam.transform.forward;
            direction.Normalize();
            Debug.Log(direction);
            rb.AddForce(direction * power * power);
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
