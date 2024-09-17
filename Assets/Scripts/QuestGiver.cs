using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    
    [SerializeField] private GameObject textPopup;

    //Om spelaren går in i området visas rutan.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(true);
        }
    }

    //Om spelaren går ut ur området försvinner rutan.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(false);
        }
    }
}
