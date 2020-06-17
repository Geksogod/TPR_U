using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Unity.Collections;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject resourcesPanel;
    public enum ResourcesType
    {
        Wood,
        Stone
    }
    [SerializeField]
    private List<Inventory> storages = new List<Inventory>();
    [Header("Storage Info")]
    [SerializeField]
    [ReadOnly]
    private int woodCount;
    [SerializeField]
    [ReadOnly]
    private int stoneCount;
    public Inventory storage;
    private ResourcesMonitorUI[] resourcesMonitor;

    private static ResourcesManager s_Instance = null;

    public static ResourcesManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(ResourcesManager)) as ResourcesManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("BuildingManager");
                s_Instance = obj.AddComponent<ResourcesManager>();
            }
            s_Instance.Initialize();
            return s_Instance;
        }
    }

    private void Initialize()
    {
        resourcesMonitor = resourcesPanel.GetComponentsInChildren<ResourcesMonitorUI>();
    }

    public void InitializationStorage(Inventory storageInventory , BuildingData buildingData)
    {
        if (buildingData.GetBuildingType() != BuildingManager.BuildingType.Storage)
            throw new System.Exception("Only storage can use InitializationStorage");
        if (!storages.Contains(storageInventory)) {
            storages.Add(storageInventory);
        }
        else
            throw new System.Exception(storageInventory.gameObject.name + " Already Initialization");
    }

    public void UpdateResourcesInfo()
    {
        if (storage != null)
        {
            woodCount = storage.GetCountOfResourcesType(ResourcesType.Wood);
            resourcesMonitor.FirstOrDefault(a => a.GetResourcesMonitorType() == ResourcesType.Wood).ChangeResourcesValue(woodCount);
            stoneCount = storage.GetCountOfResourcesType(ResourcesType.Stone);
            resourcesMonitor.FirstOrDefault(a => a.GetResourcesMonitorType() == ResourcesType.Stone).ChangeResourcesValue(stoneCount);
        }
    }

    public bool IsEnough(ResourcesType resourcesType,int needResources)
    {
        switch (resourcesType)
        {
            case ResourcesType.Wood:
                return woodCount >= needResources;
            case ResourcesType.Stone:
                return stoneCount >= needResources;
        }
        return false;
    }

    public void GetResources(ResourcesType resourcesType, int needResources)
    {
        switch (resourcesType)
        {
            case ResourcesType.Wood:
                woodCount -= needResources;
                break;
            case ResourcesType.Stone:
                stoneCount -= needResources;
                break;
        }
        UpdateResourcesInfo();
    }
}
