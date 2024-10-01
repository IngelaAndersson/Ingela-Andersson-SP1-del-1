using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] private float smoothing = 1.0f;
    public Vector3 newPosition;

    void LateUpdate()
    {
        //Kamerans position flyttar vi till en ny position, allts� newPosition.
        newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime); 
        transform.position = newPosition;
    }
}


