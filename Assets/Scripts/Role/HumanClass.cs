using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Creature))]
public class HumanClass : MonoBehaviour
{
    [SerializeField]
    protected Task currentTask;
    private bool isTaskStart;
    private bool isCourotinoStarted;
    [SerializeField]
    private float wokrKD;
    protected bool workerIsReady;

    private void Awake()
    {
        InitializationHumanClass();
    }

    private void Update()
    {
        if (!isCourotinoStarted && currentTask != null && isTaskStart)
        {
            StartCoroutine(WorkingKD(wokrKD));
            isCourotinoStarted = true;
        }
        else if(isCourotinoStarted&& !isTaskStart)
        {
            StopCoroutine(WorkingKD(wokrKD));
            isCourotinoStarted = false;
        }
    }

    public void FinishCurrentTask()
    {
        FinisedTask();
        currentTask = null;
    }

    public bool IsFree()
    {
        return currentTask == null || currentTask.GetTaskType() == TaskManager.TaskType.None;
    }

    public virtual void StartDoTask()
    {
        isTaskStart = true;
    }
    protected virtual void FinisedTask()
    {

    }
    public virtual void DoindTask()
    {

    }

    public void StopTask()
    {
        isTaskStart = false;
    }

    /// <summary>
    /// basically use AddTask()
    /// </summary>
    /// <param name="task"></param>
    public void SetCurentTask(Task task)
    {
        if (task == null)
            throw new PersonException("new task == null");

        currentTask = task;
        StartDoTask();
    }

    protected virtual void InitializationHumanClass() { }

    private IEnumerator WorkingKD(float workingKD)
    {
        while (true)
        {
            yield return new WaitForSeconds(workingKD);
            DoindTask();
        }
    }
}


