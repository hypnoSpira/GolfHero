using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	private const float BOUND_X = 22.0f;
	private const float BOUND_Z = 32.0f;
	private const float CLOUD_SPD = 25.0f;

	private Rigidbody rb;
	private Renderer rend;
	private float posX;
	private float posZ;
	private Vector3 winDir;
	private Vector3 windChk;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
		winDir = new Vector3 (1, 0, 0);
		windChk = winDir;
		rb.AddForce (winDir * CLOUD_SPD);
	}
	
	// Update is called once per frame
	void Update () {
//		winDir = HitBallBehaviour.windDir;
//		// Check for change in winDir, apply force if true
//		if (windChk != winDir) {
//			rb.velocity = Vector3.zero;
//			rb.angularVelocity = Vector3.zero;
//			rb.AddForce (winDir * CLOUD_SPD);
//			windChk = winDir;
//		}

		posX = transform.position.x;
		posZ = transform.position.z;

		// Loop cloud to opposite side if it reaches bound
		if (posX > BOUND_X) {
			Vector3 temp = transform.position;
			temp.x = BOUND_X - ((2 * BOUND_X) - 1);
			transform.position = temp;
		} else if (posX < -(BOUND_X)) {
			Vector3 temp = transform.position;
			temp.x = BOUND_X - 1;
			transform.position = temp;
		}
		if (posZ > BOUND_Z) {
			Vector3 temp = transform.position;
			temp.z = BOUND_Z - ((2 * BOUND_Z) - 1);
			transform.position = temp;
		} else if (posZ < -(BOUND_Z)) {
			Vector3 temp = transform.position;
			temp.z = BOUND_Z - 1;
			transform.position = temp;
		}
	}
}
