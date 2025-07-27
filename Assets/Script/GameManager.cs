using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action onGameWin;
    public event Action onGamePlay;
    private int level;
    private int levelCount = 20;
    [SerializeField]
    private GameState GameState;
    [SerializeField]
    LevelManager levelManager;
    public int Level {get { return level;} private set {}}
    public int LevelCount {get { return levelCount;} private set {}}

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeGameState(GameState gameState)
    {
        if (gameState == GameState) return;
        GameState = gameState;
        switch (GameState)
        {
            case GameState.Play:
                onGamePlay?.Invoke();
                break;
            case GameState.Win:
                onGameWin?.Invoke();
                break;
        }
    }
    public bool IsGamePlay()
    {
        return GameState == GameState.Play;
    }
    public void SpawnLevel(int level)
    {
        this.level = level;
        levelManager.SpawnLevel(level);
        ChangeGameState(GameState.Play);
    }
    public void ResetGameState()
    {
        ChangeGameState(GameState.None);
    }
}
public enum GameState {None, Play, Win }
