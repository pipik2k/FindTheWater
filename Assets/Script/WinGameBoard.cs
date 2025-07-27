using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameBoard : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] Text levelTxt;
    void Start()
    {
        if (retryButton != null)
            retryButton.onClick.AddListener(RetryLevel);
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void RetryLevel()
    {
        var level = GameManager.Instance.Level;
        SetLevel(level);
    }

    private void NextLevel()
    {
        var level = GameManager.Instance.Level+1;
        SetLevel(level);
    }

    public void UpdateText()
    {
        levelTxt.text = $"Level {GameManager.Instance.Level}";
    }
    private void SetLevel(int level)
    {
        GameManager.Instance.SpawnLevel(level);
        this.gameObject.SetActive(false);
    }
}
