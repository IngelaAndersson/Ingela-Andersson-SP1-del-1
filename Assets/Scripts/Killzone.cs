using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    //Denna kod k�rs n�r objektet tr�ffar en viss sak. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //F�rst m�ste vi vara s�kra p� att det �r player som tr�ffat detta omr�de.
        //Om det �r player, d� vill vi g�ra n�got. 
        if(other.CompareTag("Player"))
        {
            //FLytta spelarens position till spawn position.
            other.transform.position = spawnPosition.position;
            //N�r vi respawnar bromsar karakt�ren in lite.
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
