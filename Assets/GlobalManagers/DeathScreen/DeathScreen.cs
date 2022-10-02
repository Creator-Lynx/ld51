using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text Higscore;
    public Text Score;
    public Text Kills;

    public void Init(GameManager gm)
    {
        gameObject.SetActive(true);
        Higscore.text = $"HIGHSCORE: {gm.Highcore}";
        Score.text = $"SCORE: {gm.Score}";
        Kills.text = $"KILLS: {gm.KillCount}";
    }

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
