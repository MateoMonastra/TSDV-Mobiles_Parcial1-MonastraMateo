using UnityEngine;

[CreateAssetMenu(fileName = "FinalScore", menuName = "Game/FinalScore")]
public class FinalScore : ScriptableObject
{
    private float _player1Score;
    private float _player2Score;

    public void SetPlayer1Score(float player1Score)
    {
        _player1Score = player1Score;
    }
    
    public void SetPlayer2Score(float player2Score)
    {
        _player2Score = player2Score;
    }

    public float GetPlayer1Score()
    {
        return _player1Score;
    }

    public float GetPlayer2Score()
    {
        return _player2Score;
    }
}
