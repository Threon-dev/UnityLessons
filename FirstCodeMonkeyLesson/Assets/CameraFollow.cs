using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    private Camera myCamera;
    private Func<Vector3> GetFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    public void Setup(Func<Vector3> GetFollowPositionFunc, Func<float> GetCameraZoomFunc)
    {
        this.GetFollowPositionFunc = GetFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }

    private void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    public void SetCameraFollowPosition(Vector3 cameraFollowPosition)
    {
        SetGetCameraFollowPositionFunc(() => cameraFollowPosition);
    }
    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetFollowPositionFunc)
    {
        this.GetFollowPositionFunc = GetFollowPositionFunc;
    }

    public void SetCameraZoom(float cameraZoom)
    {
        SetGetCameraZoomFunc(() => cameraZoom);
    }
    public void SetGetCameraZoomFunc(Func<float> GetCameraZoomFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }
    void Update()
    {
        HandleMovement();
        HandleZoom();

    }

    private void HandleMovement()
    {
        Vector3 cameraFollowPosition = GetFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2f;

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                //OvershottheTarget;

                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

    void HandleZoom()
    {
        float cameraZoom = GetCameraZoomFunc();

        float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;
        float cameraZoomSpeed = 1f; 
        myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;
        if(cameraZoomDifference > 0)
        {
            if(myCamera.orthographicSize > cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (myCamera.orthographicSize < cameraZoom)
            {
                myCamera.orthographicSize = cameraZoom;
            }
        }
    }
}
