using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindText : MonoBehaviour {

    private static GameObject[] windTexts;

    // Use this for initialization
    void Start () {
        windTexts = GameObject.FindGameObjectsWithTag("Wind Text");
    }
	
    public static void SetText(string windTxt) {
        foreach (GameObject g in windTexts)
        {
            g.GetComponent<Text>().text = windTxt;
        }
    }
}
