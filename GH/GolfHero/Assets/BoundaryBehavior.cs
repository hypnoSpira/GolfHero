using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerExit(Collider other) {
        if (other.gameObject.Equals(GameObject.Find("Ball"))){
			HitBallBehaviour.resetBall();
			Debug.Log("Ball has gone out of bounds");
		}
    }
	// Update is called once per frame
	void Update () {
		
	}
}
