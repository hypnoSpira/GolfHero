using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour {
	public float multiplier;
	public float addition;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = this.gameObject.GetComponent<Renderer> ();
	}
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(GameObject.Find("Ball"))){
			HitBallBehaviour.boostCurrentForce(multiplier, addition);
			Debug.Log("Ball entered");
		}
    }
	
	// Update is called once per frame
	void Update () {
		Vector2 temp = rend.material.mainTextureOffset;
		temp -= new Vector2 (0.0f, 0.007f);
		Debug.Log (rend.material.mainTextureOffset);
		rend.material.mainTextureOffset = temp;
	}
}
