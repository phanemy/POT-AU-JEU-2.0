using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 movement;
    private Transform camTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3( speed * inputX,  speed * inputY,0) * Time.deltaTime;
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
    }
}
