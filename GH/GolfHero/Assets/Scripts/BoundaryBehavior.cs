using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<BallManager> ().getPlayerManager ().CmdResetBall ();
		}
    }
	// Update is called once per frame
	void Update () {
		
	}
}
