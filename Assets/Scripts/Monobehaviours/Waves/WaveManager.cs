using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Wave))]
public class WaveManager : MonoBehaviour
{
  public enum WaveState
    {
        preWave,
        running,
    }

    public WaveState waveState = WaveState.preWave;

    public Wave wave;

    public SpawnController spawnController;

    public delegate void ChangeWaveState(WaveState state);

    public static ChangeWaveState changeWaveState;

    private void Start()
    {
        changeWaveState += UpdateWaveState;
    }

    public void UpdateWaveState(WaveState newState)
    {
        waveState = newState;
        Debug.Log("WaveState changed to " + waveState);
        switch (waveState) 
        {
            case WaveState.preWave:
                spawnController.StopSpawning();
                if (wave.waveNum == 0)
                {
                    wave.InitFirstWave();
                }
                else
                    wave.ProgressWave();

                changeWaveState.Invoke(WaveState.running);
                break;

            case WaveState.running:
                spawnController.StartMonsterWithGapCoRoutine(spawnController.SpawnGap);
                break;

            default: break;
        }

    }

    private void OnDestroy()
    {
        changeWaveState -= UpdateWaveState;
    }
}
