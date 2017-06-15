using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	public Transform arrowTransform;
	public Transform ballTransform;
	private Transform camTransform;

	private float camRotationY;

	// Use this for initialization
	void Start () {
		camTransform = transform;
		camRotationY = 0;
	}
	
	// Update is called once per frame
	void Update () {
		camRotationY = camTransform.eulerAngles.y;
	}

	void LateUpdate () {
		Vector3 offset = (arrowTransform.forward * -2);
		arrowTransform.rotation = Quaternion.Euler (0, camRotationY - 180, 0);
		arrowTransform.position = ballTransform.position + offset;
	}
}
