using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Highcore { get; private set; }
    public int Score { get; private set; } = 0;
    public int KillCount { get; private set; } = 0;
    public DeathScreen DeathScreen;

    private string higscorePath =>
        Application.streamingAssetsPath + "/highscore.txt";

    private void Awake()
    {        
        if(!File.Exists(higscorePath))
        {
            Highcore = 0;
        }
        else
        {
            Highcore = int.Parse(File.ReadAllText(higscorePath));
        }

        FindObjectOfType<PlayerController>().OnDeath += OnPlayerDead;
    }

    public void AddScore(int score)
    {
        Score += score;
        KillCount++;
    }

    private void OnPlayerDead()
    {
        if(Score > Highcore)
        {
            File.WriteAllText(higscorePath, Score.ToString());
        }

        DeathScreen.Init(this);
    }
}
