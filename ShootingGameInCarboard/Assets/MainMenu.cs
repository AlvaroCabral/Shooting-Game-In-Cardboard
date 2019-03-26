﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   //Play game
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    //Quit Game
    public void QuiGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}