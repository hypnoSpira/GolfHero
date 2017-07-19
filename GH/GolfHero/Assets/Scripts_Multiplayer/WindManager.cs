using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WindManager : NetworkBehaviour {
    public static WindManager instance = null;

    // max speed
    private const int MAX_SPEED = 12;

    // maximum variation per second on each coordinate
    private const float X_VARIATION_PER_SECOND = 3.0f;
    private const float Y_VARIATION_PER_SECOND = 0f;
    private const float Z_VARIATION_PER_SECOND = 3.0f;


    [SyncVar]
    Vector3 wind;

    private GameObject[] windTexts;
    private GameObject windArrow;
	private Vector3 test = Vector3.zero;

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
        wind = Vector3.zero;
    }

    public void Update()
    {
        if (isServer && GameManager.instance.level > 0)
        {
            // vary wind randomly
            float newX = UnityEngine.Random.Range(wind.x - (X_VARIATION_PER_SECOND * Time.deltaTime), 
                wind.x + (X_VARIATION_PER_SECOND * Time.deltaTime));
            float newY = UnityEngine.Random.Range(wind.y - (Y_VARIATION_PER_SECOND * Time.deltaTime), 
                wind.y + (Y_VARIATION_PER_SECOND * Time.deltaTime));
            float newZ = UnityEngine.Random.Range(wind.z - (Z_VARIATION_PER_SECOND * Time.deltaTime),
                wind.z + (Z_VARIATION_PER_SECOND * Time.deltaTime));

            Vector3 newWind = new Vector3(newX, newY, newZ);
            
            // check/adjust to max speed
            if (newWind.magnitude > MAX_SPEED)
            {
                newWind.x = (newWind.x / newWind.magnitude) * MAX_SPEED;
                newWind.y = (newWind.y / newWind.magnitude) * MAX_SPEED;
                newWind.z = (newWind.z / newWind.magnitude) * MAX_SPEED;
            }

            //update wind
            wind = newWind;
        }

        if (isClient)
            UpdateWindUI();
    }

    private void UpdateWindUI()
    {
        SetText("Wind Speed: " + System.Math.Round(wind.magnitude, 1) + "km/h\nWind Direction: " + wind);

        UpdateArrow();
    }

    public Vector3 getWind()
    {
        return wind.normalized * wind.magnitude * wind.magnitude;
    }

	
    private void SetText(string windTxt) {
        windTexts = GameObject.FindGameObjectsWithTag("Wind Text");
        if (windTexts != null)
        {
            foreach (GameObject g in windTexts)
            {
                g.GetComponent<Text>().text = windTxt;
            }
        }
    }

    private void UpdateArrow()
    {
        windArrow = GameObject.FindGameObjectWithTag("Wind Arrow");
        if (windArrow != null)
        {
			Quaternion temp = windArrow.transform.rotation;
			temp.z = wind.z;
			windArrow.transform.rotation = temp;
        }
    }
}
