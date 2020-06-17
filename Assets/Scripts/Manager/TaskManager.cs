using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    public List<Task> tasks = new List<Task>();
    [SerializeField]
    public List<Task> freeTasks = new List<Task>();

    public enum TaskType
    {
        None,
        GetResources,
        MoveTo,
        BuildBuilding
    }

    public TaskType currentTaskType;
    private static TaskManager s_Instance = null;

    public static TaskManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(TaskManager)) as TaskManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("TaskManager");
                s_Instance = obj.AddComponent<TaskManager>();
            }

            return s_Instance;
        }
    }
    public bool EqualsType(TaskType taskType)
    {
        return currentTaskType == taskType;
    }

    public bool AddTask(TaskTarget taskTarget)
    {
        if (taskTarget.GetTaskType() != currentTaskType)
            throw new PersonException("taskTarget.GetTaskType() != currentTaskType");
        switch (taskTarget.GetTaskType())
        {
            case TaskType.GetResources:
                Task newTask = new Task(taskTarget.gameObject, taskTarget.transform.position, TaskType.GetResources);
                tasks.Add(newTask);
                if (CanFindFreeWorker())
                {
                    BindTaskToWorker(WorkerManager.FindFreeHuman(), newTask);
                    Debug.Log("Task " + newTask.GetTaskType() + " added");
                }
                else {
                    freeTasks.Add(newTask);
                    Debug.Log("Task " + newTask.GetTaskType() + " added to free task list");
                }
                return true;
            case TaskType.BuildBuilding:
                Task newTaskBuildBuilding = new Task(taskTarget.gameObject, taskTarget.transform.position, TaskType.BuildBuilding);
                tasks.Add(newTaskBuildBuilding);
                if (CanFindFreeWorker())
                {
                    BindTaskToWorker(WorkerManager.FindFreeHuman(), newTaskBuildBuilding);
                    Debug.Log("Task " + newTaskBuildBuilding.GetTaskType() + " added");
                }
                else
                {
                    freeTasks.Add(newTaskBuildBuilding);
                    Debug.Log("Task " + newTaskBuildBuilding.GetTaskType() + " added to free task list");
                }
                currentTaskType = TaskType.None;
                return true;
        }
        return false;
    }

    public void AddTask(Task newTask,HumanClass humanClass)
    {
        tasks.Add(newTask);
        BindTaskToWorker(humanClass, newTask);
        Debug.Log("Task " + newTask.GetTaskType() + " added");
    }

    private void Update()
    {
        
        CheckFinisedTask();
        CheckFreeTask();

    }

    private void CheckFreeTask()
    {
        if(freeTasks != null&& freeTasks.Count > 0&& CanFindFreeWorker())
        {
            if(freeTasks[0].humanClass != null) 
            {
                freeTasks.RemoveAt(0);
                return;
            }
            BindTaskToWorker(WorkerManager.FindFreeHuman(), freeTasks[0]);
            freeTasks.RemoveAt(0);
        }
    }

    private void CheckFinisedTask()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].humanClass == null)
                continue;
            switch (tasks[i].GetTaskType())
            {
                case TaskType.None:
                    break;
                case TaskType.GetResources:
                    if (tasks[i].GetTaskTarget().GetComponent<ResourcesMaterial>().IsFinished())
                    {
                        var humanClass = tasks[i].humanClass;
                        tasks[i].isTaskfinished = true;
                        humanClass.FinishCurrentTask();
                        tasks[i].GetTaskTarget().GetComponent<Outline>().enabled=false;
                        tasks.RemoveAt(i);
                        var storage = BuildingManager.instance.FindNearbyStorage(humanClass.gameObject.transform.position, BuildingManager.BuildingType.Storage);
                        var newTask = new Task(storage.gameObject, storage.transform.position,TaskType.MoveTo);
                        AddTask(newTask, humanClass);
                    }
                    break;
                case TaskType.MoveTo:
                    if(Vector3.Distance(tasks[i].humanClass.transform.position , tasks[i].GetTaskTarget().transform.position)<= tasks[i].humanClass.gameObject.GetComponent<CreatureMove>().GetStopingDistance())
                    {
                        tasks[i].isTaskfinished = true;
                        tasks[i].humanClass.FinishCurrentTask();
                        tasks.RemoveAt(i);
                    }
                    break;
                case TaskType.BuildBuilding:
                    if (tasks[i].GetTaskTarget().GetComponent<Building>().GetProgres() >= 0.95)
                    {
                        tasks[i].isTaskfinished = true;
                        tasks[i].humanClass.FinishCurrentTask();
                        tasks.RemoveAt(i);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void BindTaskToWorker(HumanClass freeWorker, Task task)
    {
        freeWorker.SetCurentTask(task);
        task.humanClass = freeWorker;
    }

    private static bool CanFindFreeWorker()
    {
        return WorkerManager.FindFreeHuman() != null;
    }

    public void ChangeTaskTypeToGetResources()
    {
        currentTaskType = TaskType.GetResources;
    }
    public void ChangeTaskTypeToBuildBuildingCastle()
    {
        if (!BuildManager.instance.ResourcesIsEnough(BuildingManager.BuildingType.Caste))
            return;
        currentTaskType = TaskType.BuildBuilding;
        BuildManager.instance.ActiveBuildPhase(BuildingManager.BuildingType.Caste);
    }

    public void ChangeTaskTypeToBuildBuildingFarm()
    {
        if (!BuildManager.instance.ResourcesIsEnough(BuildingManager.BuildingType.Farm))
            return;
        currentTaskType = TaskType.BuildBuilding;
        BuildManager.instance.ActiveBuildPhase(BuildingManager.BuildingType.Farm);
    }

    public void ChangeTaskTypeToBuildBuildingLibrary()
    {
        if (!BuildManager.instance.ResourcesIsEnough(BuildingManager.BuildingType.Caste))
            return;
        currentTaskType = TaskType.BuildBuilding;
        BuildManager.instance.ActiveBuildPhase(BuildingManager.BuildingType.Caste);
    }

    private void OnApplicationQuit()
    {
        s_Instance = null;
    }

}


[System.Serializable]
public class Task
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 position;
    public int priority;
    private static int id;
    private TaskManager.TaskType taskType;
    public bool isTaskfinished = false;
    public HumanClass humanClass;
    public GameObject GetTaskTarget()
    {
        return target;
    }
    public void SetTastTarget(GameObject taskTarget)
    {
        target = taskTarget;
    }
    public Vector3 GetTargetPosition()
    {
        return position;
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        position = targetPosition;
    }
    public Task(GameObject _target, Vector3 _position, TaskManager.TaskType _taskType, int _priority = 1)
    {
        target = _target;
        position = _position;
        priority = _priority;
        taskType = _taskType;
        id++;
    }
    public int GetId()
    {
        return id;
    }
    public TaskManager.TaskType GetTaskType()
    {
        return taskType;
    }
}
