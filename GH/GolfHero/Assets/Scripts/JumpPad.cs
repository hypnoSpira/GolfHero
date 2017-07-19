using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
	
	public float magnitude;
	private Rigidbody ball;

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball"){
			ball = other.GetComponent<Rigidbody> ();
			ball.AddForce(Vector3.up * magnitude * magnitude);
		}
    }
	// Update is called once per frame
	void Update () {
		
	}
}
