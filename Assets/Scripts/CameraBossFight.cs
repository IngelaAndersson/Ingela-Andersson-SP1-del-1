using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossFight : MonoBehaviour
{
    public Transform targetPosition; 
    public float targetSize = 10f;   
    public float transitionSpeed = 2f; 

    private Camera cam;
    private bool inTransition = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (inTransition)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * transitionSpeed);

            if (Vector3.Distance(cam.transform.position, targetPosition.position) < 0.1f && Mathf.Abs(cam.orthographicSize - targetSize) < 0.1f)
            {
                inTransition = false; 
            }
        }
    }

    public void StartBossCameraTransition()
    {
        inTransition = true;
    }
}
