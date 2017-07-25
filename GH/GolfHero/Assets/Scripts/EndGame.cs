using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	private const float WID = 0.7f;
	private const float HIG = 60;

	public GameObject barR;
	public GameObject barB;
	public GameObject barY;
	public GameObject barG;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	public GameObject first;
	public GameObject second;
	public GameObject third;
	public GameObject fourth;

	private GameObject[] players;
	private GameObject[] bars;
	private GameObject[] place;
	private GameObject[] balls;
	private Vector2 wide;
	private RectTransform rt;
	private RectTransform rtT;
	private RectTransform rtP;
	private int numPlayers;

	// Use this for initialization
	void Start () {
		wide = new Vector2 (Screen.width * WID, HIG);

		bars = new GameObject[4];
		players = new GameObject[4];
		place = new GameObject[4];
		balls = GameObject.FindGameObjectsWithTag ("Player");
		Debug.Log (GameObject.FindWithTag ("Player"));
		numPlayers = balls.Length;
		if (numPlayers == 0) {
			Debug.Log ("No players found");
		}
		if (numPlayers > 0) {
			bars [3] = barR;
			players [3] = player1;
			place [3] = first;
		}
		if (numPlayers > 1) {
			bars [2] = barB;
			players [2] = player2;
			place [2] = second;
		}
		if (numPlayers > 2) {
			bars [1] = barY;
			players [1] = player3;
			place [1] = third;
		}
		if (numPlayers > 3) {
			bars [0] = barG;
			players [0] = player4;
			place [0] = fourth;
		}
		foreach (GameObject i in bars) {
			rt = i.GetComponent<RectTransform> ();
			rtT = players [System.Array.IndexOf (bars, i)].GetComponent<RectTransform> ();
			rtP = place [System.Array.IndexOf (bars, i)].GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (0, 60);
			Vector2 pos = rt.anchoredPosition;
			float temp = (System.Array.IndexOf (bars, i) * 0.2f) + 0.1f;
			pos = new Vector2 (pos.x, Screen.height * temp);
			rt.anchoredPosition = pos;
			rtT.anchoredPosition = pos;
			rtP.anchoredPosition = pos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Animate bar expanding
		foreach (GameObject i in bars) {
			rt = i.GetComponent<RectTransform> ();
			if (rt.sizeDelta.x < wide.x) {
				rt.sizeDelta += new Vector2 (40, 0);
			}
		}
	}

	public void getTag () {
		Debug.Log (GameObject.FindWithTag ("Player"));
	}
}
