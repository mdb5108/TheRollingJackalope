﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform borders;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private Camera ourCamera;
	private AudioSource audioSource;
    
    private Vector2 offset;
    private bool isCrossing;
    private bool isZooming;
    private float wantedSize;
    private float deltaSize;

    void Start()
    {
        ourCamera = GetComponent<Camera>();
		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
        //offset = Vector2.zero;
        isCrossing = false;
        isZooming = false;
        wantedSize = ourCamera.orthographicSize;
        deltaSize = 0.0f;
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
            
            Vector3 destPos = destination;
			destPos.x = Mathf.Clamp(destination.x, totalBounds.min.x + ourCameraSize.x, totalBounds.max.x - ourCameraSize.x);
			if (isCrossing == true) {
				destPos.y = Mathf.Clamp(destination.y, totalBounds.min.y + ourCameraSize.y - 30, totalBounds.max.y - ourCameraSize.y);
			} else {
				destPos.y = Mathf.Clamp(destination.y, totalBounds.min.y + ourCameraSize.y, totalBounds.max.y - ourCameraSize.y);
			}

            transform.position = Vector3.SmoothDamp(transform.position, destPos, ref velocity, dampTime);

            // zooming out
            if (isZooming == true) {
                ourCamera.orthographicSize += deltaSize;
                if (ourCamera.orthographicSize >= wantedSize) {
                    isZooming = false;
                }
            }
        }
    }

    public void SetIsCrossing(bool i_isCrossing) {
        isCrossing = i_isCrossing;
    }
    public bool GetIsCrossing() {
        return isCrossing;
    }
    public void StartZoom() {

        float currentSize = ourCamera.orthographicSize;
        wantedSize = 1.5f * currentSize;
        float zoomingTime = 1.0f;
        deltaSize = (wantedSize - currentSize) / (zoomingTime * (1/Time.fixedDeltaTime));
        isZooming = true;
    }
}
