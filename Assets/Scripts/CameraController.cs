﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform borders;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private Camera ourCamera;
    
    private Vector2 offset;
    private bool isCrossing;

    void Start()
    {
        ourCamera = GetComponent<Camera>();
        offset = Vector2.zero;
    }

    void FixedUpdate ()
    {
        if (target)
        {
            Vector3 point = ourCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - ourCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            Vector2 ourCameraSize = new Vector2(ourCamera.orthographicSize*Screen.width/Screen.height, ourCamera.orthographicSize);
            Bounds totalBounds = borders.GetComponentInChildren<Collider2D>().bounds;
            foreach(var col in borders.GetComponentsInChildren<Collider2D>())
            {
                totalBounds.Encapsulate(col.bounds);
            }
            Vector2 mapExtents = (totalBounds.max - totalBounds.min) / 2;
            //Debug.Log("totalBounds.max:" + totalBounds.max);

            Vector2 limits = (mapExtents) - ourCameraSize;

            Vector3 destPos = destination;
            destPos.x = Mathf.Clamp(destination.x, -limits.x, limits.x);
            if (isCrossing == true) {
                destPos.y = Mathf.Clamp(destination.y, -100+offset.y, limits.y+offset.y);
            } else {
                //Debug.Log("limits: " + limits);
                //Debug.Log("offset: " + offset);
                destPos.y = Mathf.Clamp(destination.y, -limits.y+offset.y, limits.y+offset.y);
            }
            transform.position = Vector3.SmoothDamp(transform.position, destPos, ref velocity, dampTime);
        }
    }

    public void SetIsCrossing(bool i_isCrossing) {
        isCrossing = i_isCrossing;
    }
    public void SetOffset(Vector2 i_offset) {
        offset = i_offset;
    }
}
