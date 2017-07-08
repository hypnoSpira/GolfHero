using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {
	public static int Collected;
	// Use this for initialization
	void Start () {
		Collected = 0;
	}
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(GameObject.Find("Ball"))){
			//move to its own function
			collectCoin();
			Debug.Log("Ball collected a coin");
		}
    }
	// Update is called once per frame
	void Update () {
		//Do a nice rotate animation
		transform.Rotate(0,0, Time.deltaTime * 50);
	}

	void collectCoin(){
		//PLACEHOLDER Might need to move this to another class at another point
		Collected++;
		//Need to implement something furthe when more is settled
		Destroy(this.gameObject);//
	}
}
