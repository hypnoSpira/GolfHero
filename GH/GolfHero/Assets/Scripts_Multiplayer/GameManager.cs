using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkLobbyManager {

    public string[] levels;

    public GameObject cameraPrefab;

    // global game manager accessor
    public static GameManager instance = null;

    public int level;

    private PlayerManager[] playerManagers;

    private WindManager windManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instantiate(cameraPrefab);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    private void Start () {
        level = 0;
        /*
        if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Lobby")) {
            InvokeRepeating("UpdateWind", 0.1f, 20 / 3);
        }
        */
    }

    private void Update() {
        /*
        WindText.SetText("Wind Speed: " + windSpd[2] + "km/h\nWind Direction: " + windDir +
            PlayerController.powerText() + "\nCoins: " + CoinBehaviour.Collected);
        */
    }

    /*
    public static Vector3 getWind() {
        return windDir * windSpd[2] * windSpd[2];
    }
    */

    // for users to apply settings from their lobby player object to their in-game player object

    /*
public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
{
    base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    var cc = lobbyPlayer.GetComponent<ColorControl>();
    var player = gamePlayer.GetComponent<Player>();
    player.myColor = cc.myColor;
    return true;
}
*/

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        RefreshPlayersList();
    }

    // when scene changes, fetch spawn points and setup all balls to those spawn points
    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        for (int i = 0; i < levels.Length; i++)
        {
            if (sceneName == levels[i])
            {
                level = i + 1;
                break;
            }
        }

        // refresh spawn points
        BallsManager.instance.UpdateSpawnPoints();

        // RefreshPlayersList(true);

        // refresh players list and assign new balls
        //RefreshPlayersList(true);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);

        if (networkSceneName != levels[0])
        {
            GameObject player = conn.playerControllers[0].gameObject;
            PlayerManager playerManager = player.GetComponent<PlayerManager>();

            if (playerManager != null)
                playerManager.CmdGetBall();
        }
    }
 

    // ball fell into hole
    public void OnScoreBall(GameObject ball)
    {
        PlayerManager playerManager = ball.GetComponent<BallManager>().getPlayerManager();
        playerManager.scored = true;

        // destroy ball
        NetworkServer.Destroy(ball);

        // load next level if all players have scored
        if (AllPlayersScored())
        {
            LoadLevel(level + 1);
        }
    }

    // check if all players scored
    private bool AllPlayersScored()
    {
        RefreshPlayersList();

        foreach (PlayerManager playerManager in playerManagers)
            if (!playerManager.scored)
                return false;

        return true;
    }

    // refresh players list, optionally assign new balls/reset score bool
    private void RefreshPlayersList(bool assignBalls = false)
    {
        // refresh players list and assign new balls
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerManagers = new PlayerManager[players.Length];
        for (int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i] = players[i].GetComponent<PlayerManager>();
            if (assignBalls)
            {
                playerManagers[i].scored = false;
                playerManagers[i].CmdGetBall();
            }
        }
    }

    private void LoadLevel(int levelNumber)
    {
        if (levelNumber > levels.Length)
            EndMatch();
        else if (levelNumber > 0)
            ServerChangeScene(levels[levelNumber - 1]);
    }

    private void EndMatch()
    {

        RefreshPlayersList();
 
        if (playerManagers.Length == 0)
        {
            Debug.Log("No players? Something is wrong!");
            return;
        }

        int minStrokes = playerManagers[0].strokes;
        foreach (PlayerManager player in playerManagers)
        {
            if (player.strokes < minStrokes)
                minStrokes = player.strokes;
        }

        List<PlayerManager> winners = new List<PlayerManager>();
        foreach (PlayerManager player in playerManagers)
        {
            if (player.strokes == minStrokes)
                winners.Add(player);
        }

        Debug.Log("Match ended!");

        // winners contains winning players
    }
}
