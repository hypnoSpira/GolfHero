using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject stroke1;
	public GameObject stroke2;
	public GameObject stroke3;
	public GameObject stroke4;

	private GameObject[] players;
	private GameObject[] bars;
	private GameObject[] place;
	private GameObject[] balls;
	private GameObject[] strokes;
	private int[] scores;
	private Vector2 wide;
	private RectTransform rt;
	private RectTransform rtT;
	private RectTransform rtP;
	private RectTransform rtS;
	private Text txt;
	private Text tx;
	private Text xt;
	private int numPlayers;
	private int minStroke;
	private int result;
	private bool[] placed;
	private int counter;
	private int bols;
	private int idx;
	private int prev;

	// Use this for initialization
	void Start () {
		wide = new Vector2 (Screen.width * WID, HIG);

		balls = GameObject.FindGameObjectsWithTag ("Player");
		numPlayers = balls.Length;
		bars = new GameObject[numPlayers];
		players = new GameObject[numPlayers];
		place = new GameObject[numPlayers];
		strokes = new GameObject[numPlayers];
		scores = new int[numPlayers];
		placed = new bool[numPlayers];
		if (numPlayers == 0) {
			Debug.Log ("No players found");
		}

		if (numPlayers > 0) {
			bars [0] = barR;
			players [0] = player1;
			place [0] = first;
			strokes [0] = stroke1;
			placed [0] = false;
		}
		if (numPlayers > 1) {
			bars [1] = barB;
			players [1] = player2;
			place [1] = second;
			strokes [1] = stroke2;
			placed [1] = false;
		}
		if (numPlayers > 2) {
			bars [2] = barY;
			players [2] = player3;
			place [2] = third;
			strokes [2] = stroke3;
			placed [2] = false;
		}
		if (numPlayers > 3) {
			bars [3] = barG;
			players [3] = player4;
			place [3] = fourth;
			strokes [3] = stroke4;
			placed [3] = false;
		}

		// Determine Placings
		foreach (GameObject i in balls) {
			result = i.GetComponent<PlayerManager> ().strokes;
			scores [System.Array.IndexOf (balls, i)] = result;
			tx = strokes [System.Array.IndexOf (balls, i)].GetComponent<Text> ();
			tx.text = "Total Strokes: " + result;
		}
		minStroke = 9999;
		prev = minStroke;
		counter = 1;
		bols = 0;
		while (bols != numPlayers) {
			foreach (int i in scores) {
				if (placed [System.Array.IndexOf (scores, i)] == false && i < minStroke) {
					minStroke = i;
					idx = System.Array.IndexOf (scores, i);
				}
			}
			xt = place [idx].GetComponent<Text> ();
			if (scores [idx] == prev) {
				counter -= 1;
				xt.text = "" + counter;
				counter += 1;
				placed [idx] = true;
				bols += 1;
				prev = minStroke;
			} else {
				xt.text = "" + counter;
				counter += 1;
				placed [idx] = true;
				bols += 1;
				prev = minStroke;
			}
		}

		Debug.Log (numPlayers);
		foreach (GameObject i in bars) {
			Debug.Log (i);
			rt = i.GetComponent<RectTransform> ();
			rtT = players [System.Array.IndexOf (bars, i)].GetComponent<RectTransform> ();
			rtP = place [System.Array.IndexOf (bars, i)].GetComponent<RectTransform> ();
			rtS = strokes [System.Array.IndexOf (bars, i)].GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (0, 60);
			Vector2 pos = rt.anchoredPosition;
			float temp = ((-(System.Array.IndexOf (bars, i)) + 3) * 0.2f) + 0.1f;
			pos = new Vector2 (0, Screen.height * temp);
			rt.anchoredPosition = pos;
			pos = new Vector2 (pos.x, Screen.height * temp);
			rtT.anchoredPosition = pos;
			rtP.anchoredPosition = pos;
			rtS.anchoredPosition = pos;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Animate bar expanding
		foreach (GameObject i in bars) {
			rt = i.GetComponent<RectTransform> ();
			txt = place [System.Array.IndexOf (bars, i)].GetComponent<Text> ();
			if (rt.sizeDelta.x < wide.x) {
				rt.sizeDelta += new Vector2 (40, 0);
			} else if (txt.color.a < 255) {
				Color tem = txt.color;
				tem.a += 0.08f;
				txt.color = tem;
			}
		}
	}
}
