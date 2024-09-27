using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject AreYouSure;
   public void BackToMenu()
   {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

   public void showPanel()
   {
        AreYouSure.SetActive(true);
        Time.timeScale = 0;
   }

   public void closePanel()
   {
        AreYouSure.SetActive(false);
        Time.timeScale = 1;
    }
}
