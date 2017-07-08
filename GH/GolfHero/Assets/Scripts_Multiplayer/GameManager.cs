using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkManager {
    public static GameManager instance = null;
    public int defaultLayer;

    private int level;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        level = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ball fell into hole
    public void ScoreBall(GameObject ball)
    {
        // TEST CODE

        // currently just cycling right away when first ball enters


        PlayerManager playerManager = ball.GetComponent<BallManager>().getPlayerManager();

        // sometimes TriggerExit doesn't get called so we make sure layer info is correct for ball
        ball.layer = defaultLayer;


        if (level == 0)
        {
            LoadLevel1();
        } else if (level == 1)
        {
            LoadLevel2();
        }

        playerManager.CmdResetBall();
    }

    // when scene changes, fetch spawn points and setup all balls to those spawn points
    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        // setup spawn points
        BallsManager.instance.FetchSpawns();

        // reset all balls
        BallsManager.instance.ResetBalls();
    }

    private void LoadLevel1()
    {
        // change to level 1
        ServerChangeScene("MainGame_Level1");

        level = 1;
    }

    private void LoadLevel2()
    {
        // change to level 1
        ServerChangeScene("MainGame_Level2");


        level = 2;
    }
}
