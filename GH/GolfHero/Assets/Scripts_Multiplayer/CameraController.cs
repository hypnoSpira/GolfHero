using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    private const float DISTANCE_MIN = 2f;
    private const float DISTANCE_MAX = 16f;

    // the camera will not be allowed to pass through objects
    // belonging to any of these layers
    public string[] collisionLayers;

    public float heightOffset;
    public float sensitivityX;
    public float sensitivityY;
    public float sensitivityZoom;

    // target transform that the camera is pointed at
    public Transform target;

    // alpha for objects in the way (range: 0.0 to 1.0)
    public float xRayOpacity;



    private int layerMask;
    private List<Renderer> hiddenRends;
    private float currentDistance;

    public bool cameraLock;
    private float currentX;
    private float currentY;

    // arrow vars
    private Transform arrowTransform;
    private Renderer arrowRend;
    private bool arrowVisible;
    private bool arrowLock;

	// timer vars
	//private Transform timerTransform;
	//private SpriteRenderer timerRend;
	//private Sprite[] sprites;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public void ShowArrow()
    {
        if (arrowVisible)
            return;

        arrowRend.enabled = true;
		//timerRend.enabled = false;
        arrowVisible = true;
    }

    public void HideArrow()
    {
        if (!arrowVisible)
            return;

        arrowRend.enabled = false;
		//timerRend.enabled = true;
        arrowVisible = false;
    }

    public void SetArrowIntensity(Color color)
    {
        if (arrowRend)
        {
            arrowRend.material.color = color;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LockArrow()
    {
        arrowLock = true;
    }

    public void UnlockArrow()
    {
        arrowLock = false;
    }

    public void LockCamera()
    {
        cameraLock = true;
    }

    public void UnlockCamera()
    {
        cameraLock = false;
    }

    // Use this for initialization
    private void Start()
    {
        // ballcam setup
        currentDistance = (DISTANCE_MAX - DISTANCE_MIN) / 2.0f;
        currentX = 0.0f;
        currentY = 0.0f;
        hiddenRends = new List<Renderer>();
        xRayOpacity = Mathf.Clamp(xRayOpacity, 0.0f, 1.0f);
        layerMask = LayerMask.GetMask(collisionLayers);

        // arrow setup
        GameObject arrow = GameObject.FindGameObjectWithTag("Arrow"); // init arrow var
        this.arrowTransform = arrow.transform;
        this.arrowRend = arrow.GetComponent<Renderer>();
        this.arrowVisible = false;
        this.arrowLock = false;
        this.cameraLock = false;
    
		//// timer setup
		//GameObject timer = GameObject.FindGameObjectWithTag ("Timer");
		//this.timerTransform = timer.transform;
		//this.timerRend = timer.GetComponent<SpriteRenderer> ();
		//sprites = Resources.LoadAll<Sprite> ("timer");
		//Debug.Log (sprites[0]);
    }

    // Update is called once per frame
    private void Update()
    {
        if (cameraLock)
        {
            return;
        }
        // read inputs and update parameters of camera
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityZoom;

        // clamp values if under/over limits (don't want to zoom too far out or rotate camera too high/low)
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        currentDistance = Mathf.Clamp(currentDistance, DISTANCE_MIN, DISTANCE_MAX);
    }

    // LateUpdate is called once per frame (at the end of each frame)
    private void LateUpdate()
    {
        if (target == null)
        {
            HideArrow();
            return;
        }

        if (cameraLock)
            return;

        // Update camera position based on parameters
        Vector3 direction = new Vector3(0, 0, -currentDistance);
        Vector3 height = new Vector3(0, heightOffset, 0);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + height + (rotation * direction);
        transform.LookAt(target.position);

        // prevent camera going through specified object layers
        Collide();

        // update hidden objects
        updateHiddenObjects();

        // arrow and timer
		//Vector3 offset = (arrowTransform.up * 2);
		//int timer = PlayerController.timer;
		if (!arrowVisible || arrowLock) {
			//Vector3 raise = new Vector3 (0, 0.85f, 0);
			//timerTransform.rotation = Quaternion.Euler (this.transform.eulerAngles.x, 
			//	this.transform.eulerAngles.y, 
			//	this.transform.eulerAngles.z);
			//timerTransform.position = target.position + raise;
			//Debug.Log (timer);
			//if (timer > 75) {
			//	timerRend.sprite = sprites [2];
			//} else if (timer > 43) {
			//	timerRend.sprite = sprites [1];
			//} else {
			//	timerRend.sprite = sprites [0];
			//}
			//arrowTransform.position = target.position + offset;
			return;
		}
        Vector3 offset = (arrowTransform.up * 2);
        Quaternion.Euler(90, this.transform.eulerAngles.y + 90, 90);
        arrowTransform.rotation = Quaternion.Euler(90, this.transform.eulerAngles.y + 90, 90);
        arrowTransform.position = target.position + offset;

    }

    // Update hidden objects
    private void updateHiddenObjects()
    {

        // raycast from the camera to the target
        float distance = Vector3.Distance(target.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, distance);

        // re-show all previously hidden objects
        foreach (Renderer rend in hiddenRends)
        {
            if (rend != null)
            {
                Color tempColor = rend.material.color;
                tempColor.a = 1.0f;
                rend.material.color = tempColor;
            }
        }

        // hide all objects pierced
        List<Renderer> updatedRends = new List<Renderer>();
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend)
            {
                Color tempColor = rend.material.color;
                tempColor.a = Mathf.Clamp01(xRayOpacity);
                rend.material.color = tempColor;
                updatedRends.Add(rend);
            }
        }

        // book-keep hidden objects for next update
        hiddenRends = updatedRends;
    }

    // disallow movement through specified object layers
    private void Collide()
    {
        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit, layerMask))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

}
