using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private float wipeDuration = 0.5f;
    [SerializeField] WinGameBoard WinGameBoard;
    [SerializeField] GameObject gamePlayUI;

    private void Start()
    {
        GameManager.Instance.onGameWin += OpenGameBoard;
        GameManager.Instance.onGamePlay += OpenGamePlayUI;
    }

    private void OpenGamePlayUI()
    {
        gamePlayUI.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameWin -= OpenGameBoard;
    }
    public void SwipePanels(int direction)
    {
        if (panels == null || panels.Count != 2) return;

        int currentIndex = panels[0].activeSelf ? 0 : 1;
        int nextIndex = (currentIndex + 1) % 2;

        RectTransform currentRT = panels[currentIndex].GetComponent<RectTransform>();
        RectTransform nextRT = panels[nextIndex].GetComponent<RectTransform>();
        if (currentRT == null || nextRT == null) return;

        float width = currentRT.rect.width;
        Vector2 offScreenPos = new Vector2(-width, 0);
        Vector2 onScreenPos = Vector2.zero;

        panels[nextIndex].SetActive(true);
        nextRT.anchoredPosition = new Vector2(width, 0);

        currentRT.DOAnchorPos(offScreenPos, wipeDuration).OnComplete(() => panels[currentIndex].SetActive(false));
        nextRT.DOAnchorPos(onScreenPos, wipeDuration);
    }

    private void OpenGameBoard()
    {
        WinGameBoard.gameObject.SetActive(true);
        WinGameBoard.UpdateText();
    }
}
