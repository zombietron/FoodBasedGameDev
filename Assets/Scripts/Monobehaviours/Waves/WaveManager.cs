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

    public delegate void OnWaveStateChange(WaveState state);

    public static OnWaveStateChange onWaveStateChange;

    private void Start()
    {
        onWaveStateChange += HandleWaveChange;
        onWaveStateChange.Invoke(WaveState.preWave);
    }

    public void HandleWaveChange(WaveState state)
    {
        waveState = state;
        Debug.Log("WaveState changed to " + waveState);
        switch (state) 
        {
            case WaveState.preWave:
                spawnController.StopSpawning();
                if (wave.waveNum == 0)
                {
                    wave.InitFirstWave();
                }
                else
                    wave.ProgressWave();

                waveState = WaveState.running;
                onWaveStateChange.Invoke(waveState);
                break;

            case WaveState.running:
                spawnController.StartMonsterWithGapCoRoutine(spawnController.SpawnGap);
                break;

            default: break;
        }

    }

    private void OnDestroy()
    {
        onWaveStateChange -= HandleWaveChange;
    }
}
