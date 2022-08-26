using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPosition;

    public Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - playerPosition.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = playerPosition.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}
