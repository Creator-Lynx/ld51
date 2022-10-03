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

    public GameObject Heal;

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
            SpawnHeal();
            weaponSelectionDialog.ShowDialog();            
            yield return new WaitForSeconds(30f);
        }
    }

    private void SpawnHeal()
    {
        var pl = FindObjectOfType<PlayerController>();
        if (pl)
        {
            var hor = UnityEngine.Random.Range(-10, 10);
            var vert = UnityEngine.Random.Range(-10, 10);
            var vect = new Vector3(hor, 0, vert).normalized * 8f;
            var go = Instantiate(Heal);
            go.transform.position = vect;
        }
    }
}
