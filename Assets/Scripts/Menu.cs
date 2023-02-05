using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI difficultyText;

    public void Start()
    {
        SetDifficulty(0);
    }

    public void SetDifficulty(int difficulty)
    {
        Data.GamePlay.Level = difficulty;
        
        switch (difficulty)
        {
            case 0:
                difficultyText.text = "Simple Spells";
                break;
            case 1:
                difficultyText.text = "Advanced Spells";
                break;
            case 2:
                difficultyText.text = "Impossible Spells";
                break;
        }
    }
    public void StartGame()
    {
        Main.StartGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
