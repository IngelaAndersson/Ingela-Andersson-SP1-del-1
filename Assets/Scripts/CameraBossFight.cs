using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossFight : MonoBehaviour
{
    public Transform targetPosition; // The position the camera should move to (e.g., center of the room)
    public float targetSize = 10f;   // The size the camera should zoom to (adjust based on your room size)
    public float transitionSpeed = 2f; // How fast the camera moves and zooms

    private Camera cam;
    private bool inTransition = false;

    void Start()
    {
        // Get the camera component
        cam = Camera.main;
    }

    void Update()
    {
        // Smoothly move the camera to the target position and zoom out if in transition
        if (inTransition)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * transitionSpeed);

            // Optionally, stop the transition once it's close enough
            if (Vector3.Distance(cam.transform.position, targetPosition.position) < 0.1f && Mathf.Abs(cam.orthographicSize - targetSize) < 0.1f)
            {
                inTransition = false; // Stop the transition when camera reaches the target
            }
        }
    }

    public void StartBossCameraTransition()
    {
        // Enable the camera movement and zoom
        inTransition = true;
    }
}
