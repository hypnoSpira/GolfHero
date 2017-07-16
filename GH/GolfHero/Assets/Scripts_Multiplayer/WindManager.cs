using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WindManager : NetworkBehaviour {
    public static WindManager instance = null;
    private int[] windSpd;
    public Vector3 windDir;

    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
            Destroy(gameObject);
    }
	
    void Start()
    {
        windSpd = new int[] { 2, 12, 0 };
        InvokeRepeating("UpdateWind", 0.1f, 20 / 3);
    }

	// Update is called once per frame
	void Update () {

	}

    private void UpdateWind()
    {
        if (GameManager.instance.level != 0)
        {
            windDir = new Vector3(UnityEngine.Random.Range(-1f, 1.1f), 0, UnityEngine.Random.Range(-1f, 1.1f));
            windSpd[2] = UnityEngine.Random.Range(windSpd[0], windSpd[1] + 1);
            WindText.SetText("Wind Speed: " + windSpd[2] + "km/h\nWind Direction: " + windDir 
                + "\nCoins: " + CoinBehaviour.Collected);
        }
    }

    public Vector3 getWind()
    {
        return windDir * windSpd[2] * windSpd[2];
    }
}
