using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnection : MonoBehaviour
{
    PipeControl pipeControl;

    private void Start()
    {
        pipeControl = GetComponentInParent<PipeControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pipe = collision.GetComponentInParent<PipeControl>();
        if (pipe)
        {
            pipeControl.AddPipeToList(pipe);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var pipe = collision.GetComponentInParent<PipeControl>();
        if (pipe)
        {
            pipeControl.RemovePipe(pipe);
        }
    }
}
