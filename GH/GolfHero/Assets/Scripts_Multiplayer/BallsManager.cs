using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallsManager : NetworkBehaviour {

    public static BallsManager instance = null;

    // prefab of ball to spawn
    public GameObject ballPrefab;

    // array of spawn points
    private Transform[] spawnPoints;

    private int spawnLevel;

    // keeps track of all balls in play
    //private List<GameObject> balls;

    // keeps track of all unassigned balls (not used atm)
    //private Queue<GameObject> unassignedBalls;

    private int spawnCounter;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
            Destroy(gameObject);
    }

    // server start-up initialization
    public override void OnStartServer () {
        spawnCounter = 0;
        spawnLevel = 0;

        //balls = new List<GameObject>();
        //unassignedBalls = new Queue<GameObject>();
	}

    // get a ball from the unassigned set or spawn a new one if none exist
    public GameObject GetBall()
    {
        //if (unassignedBalls.Count > 0)
            //return unassignedBalls.Dequeue();
       
        return SpawnBall();
    }

    public void ResetBall(Rigidbody ballBody)
    {
        ballBody.transform.position = spawnPoints[spawnCounter].position;
        ballBody.transform.rotation = spawnPoints[spawnCounter].rotation;
        ballBody.velocity = Vector3.zero;
        ballBody.angularVelocity = Vector3.zero;
        IncrementSpawnCounter();
    }

    /*
    public void ResetBalls()
    {
        foreach (GameObject ball in balls)
        {
            ResetBall(ball.GetComponent<Rigidbody>());
        }
    }
    */

    // spawn and return a new ball
    private GameObject SpawnBall()
    {
        if (spawnPoints == null || spawnLevel != GameManager.instance.level)
        {
            UpdateSpawnPoints();
            spawnLevel = GameManager.instance.level;
        }

        var ball = (GameObject)Instantiate(ballPrefab, spawnPoints[spawnCounter].position, spawnPoints[spawnCounter].rotation);
        NetworkServer.Spawn(ball);
        //balls.Add(ball);
        IncrementSpawnCounter();
        return ball;
    }

    public void UpdateSpawnPoints()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Ball Spawn");
        spawnPoints = new Transform[spawns.Length];

        for (int i = 0; i< spawns.Length; i++)
        {
             spawnPoints[i] = spawns[i].transform;
        }
    }

    private void IncrementSpawnCounter()
    {
        spawnCounter = (spawnCounter + 1) % spawnPoints.Length;
    }
}
