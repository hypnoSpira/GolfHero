using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamController : MonoBehaviour {
    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    private const float DISTANCE_MIN = 2f;
    private const float DISTANCE_MAX = 16f;

    // target transform that the camera is pointed at
    public Transform target;

    // enables or disables see-through vision
    public bool xRayVision;

    private List<Renderer> hiddenRends;

    private float currentDistance;
    public float currentHeight;
    private float currentX;
    private float currentY;
    private float sensitivityX;
    private float sensitivityY;
    private float sensitivityZoom;

    // Use this for initialization
    private void Start () {
        currentDistance = 9.0f;
        currentX = 0.0f;
        currentY = 0.0f;
        sensitivityX = 1.0f;
        sensitivityY = 1.0f;
        sensitivityZoom = 6.0f;
        hiddenRends = new List<Renderer>();
    }

    // Update is called once per frame
    private void Update() {
        // read inputs and update parameters of camera
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityZoom;

        // clamp values if under/over limits (don't want to zoom too far out or rotate camera through ground for example)
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        currentDistance = Mathf.Clamp(currentDistance, DISTANCE_MIN, DISTANCE_MAX);
    }

    // LateUpdate is called once per frame (at the end of each frame)
    private void LateUpdate () {
        // Update camera position based on parameters
        Vector3 direction = new Vector3(0, 0, -currentDistance);
        Vector3 height = new Vector3(0, currentHeight, 0);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + height + (rotation * direction);
        transform.LookAt(target.position);

        // update hidden objects if applicable
        if (xRayVision) {
            updateHiddenObjects();
        }
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
            Color tempColor = rend.material.color;
            tempColor.a = 1.0f;
            rend.material.color = tempColor;
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
                tempColor.a = 0.1f;
                rend.material.color = tempColor;
                updatedRends.Add(rend);
            }
        }

        // book-keep hidden objects for next update
        hiddenRends = updatedRends;
    }

}
