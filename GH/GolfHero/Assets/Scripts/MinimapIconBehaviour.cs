using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconBehaviour : MonoBehaviour {
    private Quaternion fixRotation;


	// Use this for initialization
	void Start () {
        fixRotation = transform.rotation;
	}

    // LateUpdate is called once per frame (at the end of each frame)
    void LateUpdate () {
        transform.rotation = fixRotation;
    }
}
