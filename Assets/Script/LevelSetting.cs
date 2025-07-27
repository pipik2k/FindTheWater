using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public float cellSize = 1f;       
    public float padding = 1;       

    private void Awake()
    {
        SetCameraByGridType();
    }

    private void Start()
    {
        foreach (var pipe in endPipes)
        {
            pipe.onEndPipeConnected += GameWin;
        }
    }

    void SetCameraByGridType()
    {
        int width = 3, height = 3;
        switch (gridType)
        {
            case GridType.ThreeByThree: width = height = 3; break;
            case GridType.FourByFour: width = height = 4; break;
            case GridType.FiveByFive: width = height = 5; break;
            case GridType.SevenByFive: width = 5; height = 7; break;
        }

        SetOrthographicCamera(width, height);
    }

    void SetOrthographicCamera(int gridWidth, int gridHeight)
    {
        float targetWidth = gridWidth;
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraSize = targetWidth / (2f * screenAspect);
        Camera.main.orthographicSize = cameraSize;
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
            if (!pipe.HasWater) return false;
        }
        return true;
    }

}
