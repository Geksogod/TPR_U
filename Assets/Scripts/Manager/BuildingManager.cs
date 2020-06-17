using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public enum BuildingType
    {
        Storage,
        Library,
        Caste,
        Farm
    }
    [SerializeField]
    private List<Building> storages = new List<Building>();

    private static BuildingManager s_Instance = null;

    public static BuildingManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(BuildingManager)) as BuildingManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("BuildingManager");
                s_Instance = obj.AddComponent<BuildingManager>();
            }

            return s_Instance;
        }
    }

    public void InitBuild(Building _building,BuildingData buildingData)
    {
        switch (buildingData.GetBuildingType())
        {
            case BuildingType.Storage:
                storages.Add(_building);
                Inventory inventory = _building.gameObject.AddComponent<Inventory>();
                inventory.ChangeMaxCount(100);
                inventory.isStorage = true;
                ResourcesManager.instance.storage = inventory;
                ResourcesManager.instance.InitializationStorage(inventory, buildingData);
                break;
            default:
                break;
        }
    }

    public Building FindNearbyStorage(Vector3 _position,BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.Storage:
                return  FindNearby(_position, storages);
            default:
                return null;
        }
    }

    private Building FindNearby(Vector3 _position,List<Building> buildings)
    {
        float distance = float.MaxValue;
        Building tempBuilding = buildings[0];
        for (int i = 0; i < storages.Count; i++)
        {
            if (Vector3.Distance(storages[i].gameObject.transform.position, _position) < distance)
            {
                tempBuilding = storages[i];
            }
        }
        return tempBuilding;
    }
}
