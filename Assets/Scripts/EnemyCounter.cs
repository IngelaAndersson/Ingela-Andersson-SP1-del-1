using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public int enemiesDefeated = 0;
    public Text enemyCounterText;

    //Anropar denna metod när en enemy dödas
    public void OnEnemyDefeated()
    {
        enemiesDefeated++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        enemyCounterText.text = "Enemies defeated: " + enemiesDefeated;
    }

}
