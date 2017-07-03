using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour {
	public float multiplier;
	public float addition;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(GameObject.Find("Ball"))){
			HitBallBehaviour.boostCurrentForce(multiplier, addition);
			Debug.Log("Ball entered");
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
