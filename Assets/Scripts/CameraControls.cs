
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float camSpeed = 5.5f;
    [SerializeField] private float scaleRate = 2f;
    private bool cameraMoving = false;
    private Vector3 lastMousePos;
    private Camera cam;
    private float upperBound = 8.5f;
    private float lowerBound = -5.5f;
    private float leftBound = -27.5f;
    private float rightBound = 26.5f;
    private float maxOrthoSize = 6f;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        if (Input.GetMouseButton(1))
        {
            cameraMoving = true;
        }

        if (Input.GetMouseButton(1))
        {
            transform.position -= (GetMouseWorldPosition() - lastMousePos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            cameraMoving = false;
        }

        if (transform.position.y > upperBound - height/2)
        {
            transform.position = new Vector3(transform.position.x, upperBound - height / 2, transform.position.z);
        }
        else if (transform.position.y < lowerBound + height/2)
        {
            transform.position = new Vector3(transform.position.x, lowerBound + height / 2, transform.position.z);
        }
        if(transform.position.x > rightBound - width / 2)
        {
            transform.position = new Vector3(rightBound - width/2, transform.position.y, transform.position.z);
        }else if(transform.position.x < leftBound + width / 2)
        {
            transform.position = new Vector3(leftBound + width / 2, transform.position.y, transform.position.z);
        }

        if (Input.mouseScrollDelta.y != 0 && (cam.orthographicSize - Input.mouseScrollDelta.y*scaleRate) >= 0) 
            cam.orthographicSize -= Input.mouseScrollDelta.y* scaleRate;
        if (cam.orthographicSize > maxOrthoSize) cam.orthographicSize = maxOrthoSize;
        lastMousePos = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
