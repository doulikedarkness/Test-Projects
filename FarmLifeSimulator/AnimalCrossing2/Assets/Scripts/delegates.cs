using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delegates : MonoBehaviour
{
    public delegate void OnComplete();
    public OnComplete onComplete;

    public delegate void ActionOnClick();
    public event ActionOnClick onClick;

    private void Start()
    {
        //onComplete += CompletedTask;
        //onComplete += TurnOffEverything;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.X))
        //{
        //    onComplete += CompletedTask;
        //}
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    onComplete += TurnOffEverything;
        //}
        //if(Input.GetKeyDown(KeyCode.D))
        //{
        //    if(onComplete != null)
        //    {
        //        onComplete -= CompletedTask;
        //        onComplete -= TurnOffEverything;
        //    }
        //}
        //if(Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    if(onComplete != null)
        //    onComplete.Invoke();
        //}
    }

    private void CompletedTask()
    {
        Debug.Log("Task completed");
    }

    private void TurnOffEverything()
    {
        Debug.Log("Turning off scripts cause tasks all completed");
    }
}
