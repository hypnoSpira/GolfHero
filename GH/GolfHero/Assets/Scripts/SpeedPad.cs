using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour {
	public float multiplier;
	public float addition;
	private Renderer rend;
	private Rigidbody ball;

	// Use this for initialization
	void Start () {
		rend = this.gameObject.GetComponent<Renderer> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball"){
			ball = other.GetComponent<Rigidbody> ();
			Vector3 normalizedVelocity = Vector3.Normalize(ball.velocity);//Get the current velocity
			float prevMag = ball.velocity.magnitude;//Use for multiplier
			ball.AddForce(normalizedVelocity * (addition));//Perform addition first then multiply
			ball.AddForce(normalizedVelocity * ((multiplier -1f) * prevMag));
		}
    }
	
	// Update is called once per frame
	void Update () {
		// Animate texture for speed boosting appeal
		Vector2 temp = rend.material.mainTextureOffset;
		temp -= new Vector2 (0.0f, 0.007f);
		rend.material.mainTextureOffset = temp;
	}
}
