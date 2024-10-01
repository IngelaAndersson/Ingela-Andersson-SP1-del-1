using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBossFight : MonoBehaviour
{
   [SerializeField] CameraBossFight cbf;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cbf.TriggerReturnToOriginal();
    }
}
