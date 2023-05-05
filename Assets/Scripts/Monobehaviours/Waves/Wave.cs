using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave: MonoBehaviour
{

    [Tooltip("Use these to initialize your first wave")]

    public int enemyCount { private set; get; }
    public float waveTime { private set; get; }
    public int waveNum { private set; get; }
    public int maxEnemiesSpawnedDuringWave { private set; get; }
    public bool waveComplete { private set; get; }



    public int currentEnemiesSpawned { private set; get; }



    public void InitFirstWave()
    {
        enemyCount = 10;
        waveTime = 60;
        waveNum = 1;
        maxEnemiesSpawnedDuringWave = 4;
        waveComplete = false;
    }

    public void ProgressWave()
    {
        enemyCount += 5;
        waveNum++;
        maxEnemiesSpawnedDuringWave += 4;
        waveComplete = false;
        if(waveNum%3 == 0)
        {
            waveTime += 30; 
        }
    }
    
    public void AddEnemyToWaveCount()
    {
        currentEnemiesSpawned++;
        if(currentEnemiesSpawned >= enemyCount)
        {
            waveComplete = true;
        }
    }

    public void ResetWaveCount()
    {
        waveNum = 0;
    }

    
}