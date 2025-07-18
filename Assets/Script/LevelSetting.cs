using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour
{
    [SerializeField] private PipeControl[] endPipes;
    public enum GridType
    {
        ThreeByThree,
        FourByFour,
        FiveByFive,
        SevenByFive
    }
    public GridType gridType;

    private void Awake()
    {
        SetCameraByGridType();
        SetGameStart();
    }
    void Start()
    {
        foreach (var pipe in endPipes)
        {
            pipe.onEndPipeConnected += GameWin;
        }
    }

    void SetGameStart()
    {
        GameManager.Instance.ChangeGameState(GameState.Play);
    }
    void SetCameraByGridType()
    {
        switch (gridType)
        {
            case GridType.ThreeByThree:
                var fieldOfView3x3 = 30;
                SetCamera(fieldOfView3x3);
                break;
            case GridType.FourByFour:
                var fieldOfView4x4 = 39;
                SetCamera(fieldOfView4x4);
                break;
            case GridType.FiveByFive:
                var fieldOfView5x5 = 48;
                SetCamera(fieldOfView5x5);
                break;
            case GridType.SevenByFive:
                var fieldOfView7x5 = 48;
                SetCamera(fieldOfView7x5);
                break;
        }
    }

    void SetCamera(int fieldofView)
    {
        var camera = Camera.main;
        camera.fieldOfView = fieldofView;
    }
    void GameWin()
    {
        if (CheckWinGame())
        {
            GameManager.Instance.ChangeGameState(GameState.Win);
        }
    }

    bool CheckWinGame()
    {
        foreach (var pipe in endPipes)
        {
            if (!pipe.HasWater)
            {
                return false;
            }
        }
        return true;
    }

}
