using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesdData", menuName = "ScriptableObject/Resources", order = 0)]
public class ResourcesItem : ScriptableObject
{
    [SerializeField]
    private ResourcesManager.ResourcesType resourcesType;
    [SerializeField]
    private float mass;
    [SerializeField]
    private float miningDifficulty;

    public ResourcesManager.ResourcesType GetResourcesType()
    {
        return resourcesType;
    }
    public float GetMass()
    {
        return mass;
    }
    public float GetMiningDifficulty()
    {
        return miningDifficulty;
    }
}
