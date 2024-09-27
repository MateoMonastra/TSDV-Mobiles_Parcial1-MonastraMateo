using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public enum Difficult
    {
        HoraPico,
        HoraDeLaSiesta
    }

    [SerializeField] private int playerCount;
    [SerializeField] private Difficult difficulty;

    public int PlayerCount => playerCount;
    public Difficult Difficulty => difficulty;

    public void SetDifficulty(Difficult newDifficulty)
    {
        difficulty = newDifficulty;
    }

    public void SetPlayerCount(int newPlayerCount)
    {
        playerCount = newPlayerCount;
    }
}