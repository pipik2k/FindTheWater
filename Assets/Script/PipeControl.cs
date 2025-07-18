using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PipeControl : MonoBehaviour
{
    public event Action onEndPipeConnected;
    public enum PipeAction { Start,Normal,End}
    public PipeAction pipeAction = PipeAction.Normal;
    private bool isRotating;
    private bool hasWater;
    public bool HasWater => hasWater;
    public Sprite blankPipe;
    public Sprite waterPipe;
    private SpriteRenderer thisPipe;
    List<PipeControl> pipes = new List<PipeControl>();

    void Start()
    {
        if (pipeAction == PipeAction.Start)
            ChangeWater(true);
        thisPipe = GetComponentInChildren<SpriteRenderer>();
        ChangePipeVisual();
        RandomRotate();
    }

    private void OnMouseDown()
    {
        if (pipeAction != PipeAction.Normal) return;
        RotatePipe();
        PlayClickSfx();
    }

    void PlayClickSfx()
    {
        string clickName = "click";
        AudioManager.Instance.PlaySFX(clickName);
    }
    private void RandomRotate()
    {
        if (pipeAction != PipeAction.Normal) return;
        int[] angles = { 0, 90, 180, 270 };
        int randomIndex = UnityEngine.Random.Range(0, angles.Length);
        float zAngle = angles[randomIndex];
        this.transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }
    private void RotatePipe()
    {
        if (isRotating) return;
        isRotating = true;
        var RotateAngle = this.transform.rotation.eulerAngles + new Vector3(0, 0, 90);
        var duration = 0.2f;
        this.transform.DORotate(RotateAngle, duration)
            .OnComplete(() => { isRotating = false;});
    }

    void ChangeWater(bool hasWater)
    {
        this.hasWater = hasWater;
    }
    public void ChangePipeVisual()
    {
        thisPipe.sprite = hasWater ? waterPipe : blankPipe;
        if (pipeAction == PipeAction.End)
            onEndPipeConnected?.Invoke();
    }

    public void AddPipeToList(PipeControl pipe)
    {
        if (!pipes.Contains(pipe))
            pipes.Add(pipe);
         UpdateWaterStatusForConnectedPipes();
    }

    private void UpdateWaterStatusForConnectedPipes()
    {
        if (pipes.Count == 0 && pipeAction != PipeAction.Start)
        {
            ChangeWater(false);
            ChangePipeVisual();
            return;
        }

        var connectedPipes = new HashSet<PipeControl>();
        var queue = new Queue<PipeControl>();
        queue.Enqueue(this);

        bool hasStartPipe = false;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!connectedPipes.Add(current))
                continue;

            if (!hasStartPipe && current.pipeAction == PipeAction.Start)
            {
                hasStartPipe = true;
            }

            foreach (var neighbor in current.pipes)
            {
                if (!connectedPipes.Contains(neighbor))
                    queue.Enqueue(neighbor);
            }
        }

        foreach (var pipe in connectedPipes)
        {
            if (pipe.hasWater != hasStartPipe)
            {
                pipe.ChangeWater(hasStartPipe);
                pipe.ChangePipeVisual();
            }
        }
    }
   
    public void RemovePipe(PipeControl pipe)
    {
        pipes.Remove(pipe);
        UpdateWaterStatusForConnectedPipes();
    }


}

