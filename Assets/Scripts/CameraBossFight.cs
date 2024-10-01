using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossFight : MonoBehaviour
{
    [SerializeField] CameraFollow cameraf;
    public Transform targetPosition; 
    public float targetSize = 10f;   
    public float transitionSpeed = 2f; 


    public Camera bossCamera;
    private bool inTransition = false;

    // Declare variables to store the original camera state
    private Vector3 originalPosition;
    private float originalSize;
    private bool returnToOriginal = false;

    void Start()
    {
        // Initialize the original position and size to the camera's starting values
        originalPosition = bossCamera.transform.position;
        originalSize = bossCamera.orthographicSize;

        bossCamera = Camera.main;
    }

    void Update()
    {
        if (inTransition)
        {
            // Smooth transition to the target position and size
            bossCamera.transform.position = Vector3.Lerp(bossCamera.transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
            bossCamera.orthographicSize = Mathf.Lerp(bossCamera.orthographicSize, targetSize, Time.deltaTime * transitionSpeed);

            // Check if the camera has reached the target position and size
            if (Vector3.Distance(bossCamera.transform.position, targetPosition.position) < 0.1f && Mathf.Abs(bossCamera.orthographicSize - targetSize) < 0.1f)
            {
                inTransition = false;
            }
        }

        // Fallback to return the camera to its original state
        if (returnToOriginal)
        {
            bossCamera.transform.position = cameraf.newPosition;
            bossCamera.orthographicSize = Mathf.Lerp(bossCamera.orthographicSize, originalSize, Time.deltaTime * transitionSpeed);

            // Stop the fallback when the camera has returned to the original state
            if (Vector3.Distance(bossCamera.transform.position, originalPosition) < 0.1f && Mathf.Abs(bossCamera.orthographicSize - originalSize) < 0.1f)
            {
                returnToOriginal = false;
            }
        }
    }

    // Call this method to trigger the fallback transition
    public void TriggerReturnToOriginal()
    {
        returnToOriginal = true;
    }


    //void Start()
    //{
    //    bossCamera = Camera.main;
    //}

    //void Update()
    //{
    //    if (inTransition)
    //    {
    //        bossCamera.transform.position = Vector3.Lerp(bossCamera.transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
    //        bossCamera.orthographicSize = Mathf.Lerp(bossCamera.orthographicSize, targetSize, Time.deltaTime * transitionSpeed);

    //        if (Vector3.Distance(bossCamera.transform.position, targetPosition.position) < 0.1f && Mathf.Abs(bossCamera.orthographicSize - targetSize) < 0.1f)
    //        {
    //            inTransition = false; 
    //        }
    //    }
    //}

    public void StartBossCameraTransition()
    {
        inTransition = true;
    }

}
