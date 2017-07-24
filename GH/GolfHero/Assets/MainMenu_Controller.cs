using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
