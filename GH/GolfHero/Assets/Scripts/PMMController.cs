using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  using UnityEngine.SceneManagement;

public class PMMController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
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
    }

    // Resumes Time
    public void Resume()
    {
        // scale Time by 1 (Time flows as normal)
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")) {
            Pause();
            HitBallBehaviour.Pause();
        } else if (Input.GetKeyDown("o")) {
            Resume();
            HitBallBehaviour.Resume();
        } else if (Input.GetKeyDown("i")) {
            Reload();
        }
    }
}
