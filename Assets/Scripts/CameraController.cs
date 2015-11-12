using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform borders;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private Camera ourCamera;
	private AudioSource audioSource;

    void Start()
    {
        ourCamera = GetComponent<Camera>();
		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
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
            Vector2 mapExtents = totalBounds.max;

            Vector2 limits = (mapExtents) - ourCameraSize;

            Vector3 destPos = destination;
            destPos.x = Mathf.Clamp(destination.x, -limits.x, limits.x);
            destPos.y = Mathf.Clamp(destination.y, -limits.y, limits.y);
            transform.position = Vector3.SmoothDamp(transform.position, destPos, ref velocity, dampTime);
        }
    }
}
