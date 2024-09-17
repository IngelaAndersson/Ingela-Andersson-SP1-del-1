using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    
    [SerializeField] private GameObject textPopup;

    //Om spelaren g�r in i omr�det visas rutan.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(true);
        }
    }

    //Om spelaren g�r ut ur omr�det f�rsvinner rutan.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(false);
        }
    }
}
