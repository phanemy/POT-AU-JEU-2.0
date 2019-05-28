using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float minSize = 2;
    public float maxSize = 10;
    public float zoomSpeed = 1f;

    public CinemachineVirtualCamera cam;

    private void Awake()
    {
        if(cam == null)
            cam = this.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam != null)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            cam.m_Lens.OrthographicSize += zoom * zoomSpeed;

            if (cam.m_Lens.OrthographicSize < minSize)
                cam.m_Lens.OrthographicSize = minSize;
            else if (cam.m_Lens.OrthographicSize > maxSize)
                cam.m_Lens.OrthographicSize = maxSize;
        }
    }
}
