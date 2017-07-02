using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//  using UnityEngine.SceneManagement;

public class PMMController : MonoBehaviour
{
    GameObject[] showOnPause;

    // Use this for initialization
    void Start()
    {
        showOnPause = GameObject.FindGameObjectsWithTag("PMM_P");
        Resume();
    }

    // Reloads current scene
    public void Reload()
    {
        // reload scene breaks the shader - 
        // fix: http://answers.unity3d.com/questions/919940/applicationloadlevel-changes-lighting-for-some-rea.html
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Pause();
        HitBallBehaviour.stopBall();
        HitBallBehaviour.resetBall();
        Resume();
    }

    // Pauses Time
    public void Pause()
    {
        // scale Time by 0 (Time stops)
        Time.timeScale = 0;
        showPauseMenu();
    }

    // Resumes Time
    public void Resume()
    {
        // scale Time by 1 (Time flows as normal)
        Time.timeScale = 1;
        hidePauseMenu();
    }

    private void showPauseMenu()
    {
        foreach(GameObject o in showOnPause)
        {
            o.SetActive(true);
        }
        GameObject.Find("TogglePause").GetComponentInChildren<Text>().text = "Resume";
    }

    private void hidePauseMenu()
    {
        foreach (GameObject o in showOnPause)
        {
            o.SetActive(false);
        }
        GameObject.Find("TogglePause").GetComponentInChildren<Text>().text = "Pause";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
        else if (Input.GetKeyDown("i"))
        {
            Reload();
        }
    }
}
