using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossFight : MonoBehaviour
{
    public Transform targetPosition; 
    public float targetSize = 10f;   
    public float transitionSpeed = 2f; 

    public Camera bossCamera;
    private bool inTransition = false;

    void Start()
    {
        bossCamera = Camera.main;
    }

    void Update()
    {
        if (inTransition)
        {
            bossCamera.transform.position = Vector3.Lerp(bossCamera.transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
            bossCamera.orthographicSize = Mathf.Lerp(bossCamera.orthographicSize, targetSize, Time.deltaTime * transitionSpeed);

            if (Vector3.Distance(bossCamera.transform.position, targetPosition.position) < 0.1f && Mathf.Abs(bossCamera.orthographicSize - targetSize) < 0.1f)
            {
                inTransition = false; 
            }
        }
    }

    public void StartBossCameraTransition()
    {
        inTransition = true;
    }

    public void EndBossCameraTransition()
    {
        inTransition = false;
    }
}
