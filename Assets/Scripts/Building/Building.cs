using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingData buildingData;
    [SerializeField]
    private float progress = 0;
    private Mesh currentMesh;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    public GameObject unitToSpawn;

    private void Start()
    {
        BuildingManager.instance.InitBuild(this, buildingData);
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        //currentMesh = buildingData.GetMestFromState(0);
        //meshFilter.mesh = currentMesh;
        //meshCollider.sharedMesh = currentMesh;

    }

    public void AddProgress(float shift)
    {
        progress += shift;
        if (progress > 0.3f && progress < 0.8f && buildingData.GetBuildingStateCount() > 0 && currentMesh != buildingData.GetMestFromState(1))
        {
            currentMesh = buildingData.GetMestFromState(1);
            meshFilter.mesh = currentMesh;
            meshCollider.sharedMesh = currentMesh;
        }
        else if (progress > 0.85f && buildingData.GetBuildingStateCount() > 1 && currentMesh != buildingData.GetMestFromState(2))
        {
            currentMesh = buildingData.GetMestFromState(2);
            meshFilter.mesh = currentMesh;
            meshCollider.sharedMesh = currentMesh;
        }
    }
    public float GetProgres()
    {
        return progress;
    }

    public void SetBuildingData(BuildingData _buildingData)
    {
        buildingData = _buildingData;
    }
    public BuildingManager.BuildingType buildingType()
    {
        return buildingData.GetBuildingType();
    }

    private IEnumerator SpawnUnits(GameObject unit)
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            GameObject.Instantiate(unit, new Vector3(gameObject.transform.position.x + Random.Range(4, 5), transform.position.y, transform.position.z + Random.Range(4,5)), new Quaternion(0, 0, 0, 0));
        }
    }
}
