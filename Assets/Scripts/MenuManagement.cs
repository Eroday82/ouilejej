using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{

    public Button Jouer, Quitter;

    //send the player to the level 1
    void Start()
    {
        Jouer.onClick.AddListener(LoadLevel1);
        Quitter.onClick.AddListener(QuitGame);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
