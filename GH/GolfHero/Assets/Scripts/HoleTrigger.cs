using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour {
	/*This object is to be placed at the bottom of the golf hole and triggers any behavior for a player completing a course*/
	void OnTriggerEnter(Collider other){
		Debug.Log("Object has entered the trigger zone");
		//Need to decide on concrete design for the Host/Master object
		hitbottom(other.gameObject);
	}
	void hitbottom(GameObject other){
		//Implement whatever behavior we decide on, this is just temporary
		Destroy(other.GetComponent<Collider>());//Fake the ball falling through this is a temp fix

	}
	
	
}
