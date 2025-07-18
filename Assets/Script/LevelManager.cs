using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject level;
    public void SpawnLevel(int levelNumber)
    {
        string levelName = "Level" + levelNumber;
        GameObject levelPrefab = Resources.Load<GameObject>(levelName);
        if (levelPrefab != null)
        {
            DestroyLevel();
            level = Instantiate(levelPrefab, levelPrefab.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Level prefab '{levelName}' not found in Resources.");
        }
    }

    public void DestroyLevel()
    {
        if (level!=null)
            Destroy(level);
    }
}
