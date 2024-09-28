using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    [SerializeField] private Image singlePlayer;
    [SerializeField] private Image multiPlayer;
    
    [SerializeField] private GameSettings gameSettings;

    private void Start()
    {
        SinglePlayerSelected();
    }

    public void SinglePlayerSelected()
    {
        singlePlayer.color = Color.white;
        multiPlayer.color = Color.gray;
        gameSettings.SetPlayerCount(1);
    }
    
    public void MultiPlayerSelected()
    {
        multiPlayer.color = Color.white;
        singlePlayer.color = Color.gray;
        gameSettings.SetPlayerCount(2);
    }
}
