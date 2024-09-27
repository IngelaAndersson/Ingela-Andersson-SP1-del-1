using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField] Animator gateAnim;

    // Start is called before the first frame update
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
