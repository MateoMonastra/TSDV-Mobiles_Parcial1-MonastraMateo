using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private Image napTime;
    [SerializeField] private Image rushHour;
    
    [SerializeField] private GameSettings gameSettings;

    private void Start()
    {
        NapTimeSelected();
        
    }

    public void NapTimeSelected()
    {
        napTime.color = Color.white;
        rushHour.color = Color.gray;
        gameSettings.SetDifficulty(GameSettings.Difficult.NapTime);
    }
    
    public void RushHourSelected()
    {
        rushHour.color = Color.white;
        napTime.color = Color.gray;
        gameSettings.SetDifficulty(GameSettings.Difficult.RushHour);
    }
}
