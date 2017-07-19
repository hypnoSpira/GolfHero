using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//  using UnityEngine.SceneManagement;

public class PMMController : MonoBehaviour
{
    GameObject[] showOnPause;

    protected bool paused;

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

        
        Resume();
        HitBallBehaviour.resetBall();
    }

    // Pauses Time
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        CameraController.instance.LockCamera();
        this.paused = true;
        // HitBallBehaviour.Pause();
        showPauseMenu();
    }

    // Resumes Time
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CameraController.instance.UnlockCamera();
        this.paused = false;
        // HitBallBehaviour.Resume();
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

    public void togglePause()
    {
        if (!this.paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            togglePause();
        }
        else if (Input.GetKeyDown("i")) {
            Reload();
        }
    }
}
