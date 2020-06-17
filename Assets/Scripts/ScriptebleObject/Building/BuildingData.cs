using System.Linq;
using Unity.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObject/Building", order = 2)]
public class BuildingData : ScriptableObject
{
    [SerializeField]
    private BuildingManager.BuildingType buildingType;
    [SerializeField]
    private GameObject[] buildingState;

    [System.Serializable]
    public struct BuildingResource
    {
        [ReadOnly]
        public string resourceName;
        public ResourcesManager.ResourcesType resourcesType;
        public int count;
    }

    [SerializeField]
    public BuildingResource[] buildingResources;

    public BuildingManager.BuildingType GetBuildingType()
    {
        return buildingType;
    }

    
    public int GetBuildingStateCount()
    {
        return buildingState.Length;
    }

    public BuildingData GetBuildingData()
    {
        return this;
    }

    public BuildingResource[] GetBuildingResources()
    {
        return buildingResources;
    }

    public Mesh GetMestFromState(int stateIndex)
    {
        return buildingState[stateIndex].GetComponent<MeshFilter>().sharedMesh;
    }
    public GameObject GetGameObjectFromState(int stateIndex)
    {
        return buildingState[stateIndex];
    }

    private void OnValidate()
    {
        for (int i = 0; i < buildingResources.Length; i++)
        {
            buildingResources[i].resourceName = buildingResources[i].resourcesType.ToString();
        }
    }
}
