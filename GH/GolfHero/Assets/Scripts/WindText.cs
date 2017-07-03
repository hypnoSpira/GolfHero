using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindText : MonoBehaviour {

    private static Text txt;

	// Use this for initialization
	void Start () {
        txt = GetComponent(typeof(Text)) as Text;
	}
	
    public static void setText(string windTxt) {
        txt.text = windTxt;
    }
}
