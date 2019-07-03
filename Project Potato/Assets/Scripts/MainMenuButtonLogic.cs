using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonLogic : MonoBehaviour
{

    [SerializeField]
    private GameObject MainMenuScreen;
    [SerializeField]
    private GameObject CreditsScreen;

   public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

   public void CreditsButton()
    {
        MainMenuScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsBackButton()
    {
        MainMenuScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }
}

