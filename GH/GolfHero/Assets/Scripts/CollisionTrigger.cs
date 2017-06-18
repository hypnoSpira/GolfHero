using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

	public int defaultLayer;
	public int holeLayer;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.layer = holeLayer;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.layer = defaultLayer;
		}
	}
}
