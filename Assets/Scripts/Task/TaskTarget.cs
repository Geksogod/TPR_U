using cakeslice;
using UnityEngine;

public class TaskTarget : MonoBehaviour, ITouchListener
{
    public TaskManager.TaskType taskType;
    private Outline outline;
    private bool chosen;

    private void Awake()
    {
        outline = gameObject.GetComponent<Outline>();
    }

    public void MouseDown()
    {
        if(outline!=null)
        if (TaskManager.instance.EqualsType(taskType))
        {
            chosen = TaskManager.instance.AddTask(this);
            if (chosen)
                OutlineManager.instance.ActiveOutline(outline, true, 0);
        }
    }

    public void MouseEnter()
    {
        if (outline != null)
            if (!chosen)
            if (TaskManager.instance.EqualsType(taskType))
            {
                OutlineManager.instance.ActiveOutline(outline, true, 2);
            }
    }

    public void MouseExit()
    {
        if (outline != null)
            if (!chosen)
            if (TaskManager.instance.EqualsType(taskType))
            {
                OutlineManager.instance.ActiveOutline(outline, false);
            }
    }
    public TaskManager.TaskType GetTaskType()
    {
        return taskType;
    }
}
