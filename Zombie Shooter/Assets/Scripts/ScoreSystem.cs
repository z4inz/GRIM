using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text multiplierText;

    int score;
    int wave = 1;
    float scoreMultiplierExpiration;
    int killMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        Zombie.ZombieDied += Zombie_Died;
        GameManager.NewWave += New_Wave;
    }

    void Zombie_Died()
    {
        
        UpdateKillMultiplier();
        score += killMultiplier;

        scoreText.SetText("Score: " + score.ToString());
    }

    void UpdateKillMultiplier()
    {
        if (Time.time <= scoreMultiplierExpiration)
        {
            killMultiplier++;
        }
        else
        {
            killMultiplier = 1;
        }
        scoreMultiplierExpiration = Time.time + 1f;

        multiplierText.SetText(killMultiplier + "x");
        if (killMultiplier < 3)
        {
            multiplierText.color = Color.white;
        }
        else if (killMultiplier < 10)
        {
            multiplierText.color = Color.yellow;
        }
        else if (killMultiplier < 20)
        {
            multiplierText.color = Color.green;
        }
        else if (killMultiplier < 30)
        {
            multiplierText.color = Color.red;
        }
    }

    void New_Wave()
    {
        wave++;
        waveText.SetText("Wave " + wave.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
