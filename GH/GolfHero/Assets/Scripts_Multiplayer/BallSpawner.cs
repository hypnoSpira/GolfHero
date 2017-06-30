using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour {

    private int spawnCounter;

    // prefab of ball to spawn
    public GameObject ballPrefab;

    // array of spawn points
    public Transform[] spawnPoints;

    // keeps track of all balls in play
    public List<GameObject> balls;

    // keeps track of all unassigned balls (not used atm)
    public Queue<GameObject> unassignedBalls;

    // server start-up initialization
	public override void OnStartServer () {
        spawnCounter = 0;
        balls = new List<GameObject>();
        unassignedBalls = new Queue<GameObject>();
	}

    // get a ball from the unassigned set or spawn a new one if none exist
    public GameObject getBall()
    {
        if (unassignedBalls.Count > 0)
            return unassignedBalls.Dequeue();

        return SpawnBall();
    }

    // spawn and return a new ball
    private GameObject SpawnBall()
    {
        var ball = (GameObject)Instantiate(ballPrefab, spawnPoints[spawnCounter].position, spawnPoints[spawnCounter].rotation);
        NetworkServer.Spawn(ball);
        balls.Add(ball);
        spawnCounter = (spawnCounter + 1) % spawnPoints.Length;
        return ball;
    }
}
