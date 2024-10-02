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

  
    private Vector3 originalPosition;
    private float originalSize;
    private bool returnToOriginal = false;

    void Start()
    {
      
        originalPosition = bossCamera.transform.position;
        originalSize = bossCamera.orthographicSize;

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

      
        if (returnToOriginal)
        {
            bossCamera.transform.position = cameraf.newPosition;
            bossCamera.orthographicSize = Mathf.Lerp(bossCamera.orthographicSize, originalSize, Time.deltaTime * transitionSpeed);

            if (Vector3.Distance(bossCamera.transform.position, originalPosition) < 0.1f && Mathf.Abs(bossCamera.orthographicSize - originalSize) < 0.1f)
            {
                returnToOriginal = false;
            }
        }
    }

 
    public void TriggerReturnToOriginal()
    {
        returnToOriginal = true;
    }



    public void StartBossCameraTransition()
    {
        inTransition = true;
    }

}
