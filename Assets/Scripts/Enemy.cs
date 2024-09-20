using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyCounter enemyCounter;
    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
    }

    public void Die()
    {
        enemyCounter.OnEnemyDefeated();
        Destroy(gameObject);
    }
}
