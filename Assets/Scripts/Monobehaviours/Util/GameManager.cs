using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //setwaves into action
    //score

    public static GameManager Instance { get; private set; }

    public WaveManager waveMgr;

    public enum GameState
    {
        menu,
        gameRunning,
        gameEnding,
        pause
    }

    public GameState gameState;

    public delegate void OnGameStateChange(GameState state);

    public OnGameStateChange onGameStateChange;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
            Instance = this;


        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        //for testing while we wait to build our menu scene.
        OnSceneLoad(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    public void ChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (gameState) 
        {
            case GameState.menu:
                //do menu stuff
                break;

            case GameState.gameRunning:
                WaveManager.changeWaveState(WaveManager.WaveState.preWave);
                break;

            case GameState.gameEnding:
                EndGame();
                break;
            
            case GameState.pause:
                PauseGame();                
                break;
            
            default: break;


        }
    }

    public void EndGame()
    {
        //end the game
        Debug.Log("I'm the end of the game");
    }

    public void PauseGame()
    {
        Debug.Log("Paused");
    }
    
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"{SceneManager.GetActiveScene()} has loaded");

        if(scene.name == "Main")
        {
            ChangeGameState(GameState.gameRunning);
        }
    }
}
