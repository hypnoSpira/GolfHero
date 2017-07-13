using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    private Transform target;

    // alpha for objects in the way (range: 0.0 to 1.0)
    public float xRayOpacity;



    private int layerMask;
    private List<Renderer> hiddenRends;
    private float currentDistance;
    
    private float currentX;
    private float currentY;
    private static bool pause = false;

    // arrow vars
    private Transform arrowTransform;
    private Renderer[] arrowRends;
    private bool arrowVisible;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public void ShowArrow()
    {
        if (arrowVisible)
            return;

        foreach (Renderer rend in this.arrowRends)
        {
            rend.enabled = true;
        }

        arrowVisible = true;
    }

    public void HideArrow()
    {
        if (!arrowVisible)
            return;

        foreach (Renderer rend in this.arrowRends)
        {
            rend.enabled = false;
        }

        arrowVisible = false;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        GameObject arrow = GameObject.Find("Arrow"); // init arrow var
        this.arrowTransform = arrow.transform;
        this.arrowRends = arrow.GetComponentsInChildren<Renderer>();
        this.arrowVisible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pause)
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

        // arrow

        if (arrowVisible == false)
            return;
        Vector3 offset = (arrowTransform.forward * -2);
        arrowTransform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y - 180, 0);
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

    public static bool toggleEnable()
    {
        //pause = !pause;
        Debug.Log("Is camera enabled: " + pause);
        //return pause;
        return pause = !pause;
    }

}
