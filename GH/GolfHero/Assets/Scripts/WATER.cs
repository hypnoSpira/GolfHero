using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATER : MonoBehaviour {

	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = this.gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Animate texture for MAXIMUM WATER MOVEMENT
		Vector2 temp = rend.material.mainTextureOffset;
		temp -= new Vector2 (0.0f, 0.007f);
		rend.material.mainTextureOffset = temp;
	}
}
