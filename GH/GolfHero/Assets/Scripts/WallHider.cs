using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour {

	public Transform ballTransform;

	private float dist = 0.0f;
	private RaycastHit hit;
	private Collider target;
	private bool touch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (ballTransform.position, transform.position);
		if (Physics.Raycast (transform.position, transform.forward, out hit, dist - 1)) {
			target = hit.collider;
			Color color = target.GetComponent<Renderer> ().material.color;
			color.a = 0.1f;
			target.GetComponent<Renderer> ().material.color = color;
			touch = true;
		} else if (touch) {
			Color color = target.GetComponent<Renderer> ().material.color;
			color.a = 1.0f;
			target.GetComponent<Renderer> ().material.color = color;
			touch = false;
		}
	}
}
