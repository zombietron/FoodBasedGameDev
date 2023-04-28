using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //setwaves into action
    //score
    //players in the game

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        menu,
        gameRunning,
        gameEnding,
        pause
    }

    public GameState gameState;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
