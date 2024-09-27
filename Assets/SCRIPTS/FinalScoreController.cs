using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreController : MonoBehaviour
{
    [SerializeField] private FinalScore finalScore;

    [SerializeField] private Text scorePlayer1Text;
    [SerializeField] private Text scorePlayer2Text;

    [SerializeField] private GameObject player1WinPanel;
    [SerializeField] private GameObject player2WinPanel;

    [SerializeField] private float refreshCooldown;
    
    private GameObject _winPanel;
    
    private float _timer;

    private void Start()
    {
        scorePlayer1Text.text = $"{finalScore.GetPlayer1Score()}";
        scorePlayer2Text.text = $"{finalScore.GetPlayer2Score()}";

        if (finalScore.GetPlayer1Score() > finalScore.GetPlayer2Score())
        {
            _winPanel = player1WinPanel;
        }
        else
        {
            _winPanel = player2WinPanel;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (refreshCooldown<=_timer)
        {
            _winPanel.SetActive(_winPanel.gameObject.activeInHierarchy == false);
            _timer = 0;
        }
    }
}