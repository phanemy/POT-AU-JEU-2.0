using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float minSize = 2;
    public float maxSize = 10;
    public float zoomSpeed = 1f;

    private Camera cam;

    private void OnEnable()
    {
        cam = this.GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize += zoom * zoomSpeed;

        if (cam.orthographicSize < minSize)
            cam.orthographicSize = minSize;
        else if(cam.orthographicSize > maxSize)
            cam.orthographicSize = maxSize;
    }
}
