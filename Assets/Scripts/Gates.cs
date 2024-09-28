using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{

    [SerializeField] Animator gateAnim;

    void Start()
    {
        gateAnim = GetComponent<Animator>();
    }

    public void OpenGate()
    {
        gateAnim.SetTrigger("OpenGate");
    }
    public void CloseGate()
    {
        gateAnim.SetTrigger("CloseGate");
    }

}
