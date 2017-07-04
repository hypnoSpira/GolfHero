using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {
	public float magnitude;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(GameObject.Find("Ball"))){
			HitBallBehaviour.jump(magnitude * magnitude);
			Debug.Log("BOING BOING");
		}
    }
	// Update is called once per frame
	void Update () {
		
	}
}
