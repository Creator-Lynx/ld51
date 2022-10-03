using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Highcore { get; private set; }
    public int Score { get; private set; } = 0;
    public int KillCount { get; private set; } = 0;
    public DeathScreen DeathScreen;

    public Action OnActivePhase;
    public bool IsActivePhase { get; private set; } = false;

    public WeaponsDataBase weapDB;
    public WeaponSelectionDialog weaponSelectionDialog;

    private string higscorePath =>
        Application.streamingAssetsPath + "/highscore.txt";

    private void Awake()
    {
        weapDB = Resources.Load<WeaponsDataBase>("WeaponsDB/Weapons");
        weapDB.SetNamesAndLevels();

        if (!File.Exists(higscorePath))
        {
            Highcore = 0;
        }
        else
        {
            Highcore = int.Parse(File.ReadAllText(higscorePath));
        }

        FindObjectOfType<PlayerController>().OnDeath += OnPlayerDead;
    }

    public void StartActivePhase()
    {
        IsActivePhase = true;
        OnActivePhase?.Invoke();

        StartCoroutine(WeaponDelivery());        
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

    private IEnumerator WeaponDelivery()
    {
        yield return new WaitForSeconds(3f);

        while(true)
        {
            weaponSelectionDialog.ShowDialog();
            yield return new WaitForSeconds(30f);
        }
    }
}
