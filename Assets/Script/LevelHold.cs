using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHold : MonoBehaviour
{
    [Header("Level Button Prefab (must have a Text child)")]
    public GameObject levelButtonPrefab;

    [Header("Parent Transform for Buttons")]
    public Transform buttonParent;

    private List<GameObject> levelButtons = new List<GameObject>();


    void Start()
    {
        CreateLevelButtons(GameManager.Instance.LevelCount);
    }

    void CreateLevelButtons(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            GameObject btnObj = Instantiate(levelButtonPrefab, buttonParent);
            btnObj.name = $"LevelButton_{i}";

            // Try to find a Text component in the children (for Unity UI)
            Text text = btnObj.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = $"{i}";
            }

            // Optionally, add a listener to the button
            Button button = btnObj.GetComponent<Button>();
            if (button != null)
            {
                int levelIndex = i; // Capture for closure
                button.onClick.AddListener(() => OnLevelButtonClicked(levelIndex));
            }

            levelButtons.Add(btnObj);
        }
    }

    public void SetLevelButtonText(int levelIndex, string newText)
    {
        if (levelIndex < 1 || levelIndex > levelButtons.Count) return;
        Text text = levelButtons[levelIndex - 1].GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = newText;
        }
    }

    void OnLevelButtonClicked(int level)
    {
        Debug.Log($"Level {level} button clicked!");
        GameManager.Instance.SpawnLevel(level);
        this.gameObject.SetActive(false);
    }
}
