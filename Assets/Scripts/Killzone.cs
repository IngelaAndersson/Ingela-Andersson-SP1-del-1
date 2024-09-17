using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    //Denna kod körs när objektet träffar en viss sak. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Först måste vi vara säkra på att det är player som träffat detta område.
        //Om det är player, då vill vi göra något. 
        if(other.CompareTag("Player"))
        {
            //FLytta spelarens position till spawn position.
            other.transform.position = spawnPosition.position;
            //När vi respawnar bromsar karaktären in lite.
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
