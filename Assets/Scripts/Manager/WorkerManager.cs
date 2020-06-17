using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    private static WorkerManager s_Instance = null;
    public static WorkerManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(WorkerManager)) as WorkerManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("AManager");
                s_Instance = obj.AddComponent<WorkerManager>();
            }

            return s_Instance;
        }
    }

    public static List<WorkerClass> humanClasses = new List<WorkerClass>();
    public static void Initialize(WorkerClass newHumanClass)
    {
        humanClasses.Add(newHumanClass);
    }

    public static WorkerClass FindFreeHuman()
    {
        for (int i = 0; i < humanClasses.Count; i++)
        {
            if (humanClasses[i].IsFree())
            {
                return humanClasses[i];
            }
        }
        return null;
    }


}
