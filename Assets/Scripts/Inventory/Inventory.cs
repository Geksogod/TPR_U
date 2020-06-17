using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ResourcesItem> resourcesItems;

    [SerializeField]
    private int inventoryMaxCount = 5;
    public bool isStorage;
    [SerializeField]
    private bool isFull;


    private void Start()
    {
        resourcesItems = new List<ResourcesItem>(3);
    }
    
    public void AddItem(ResourcesItem item)
    {
        if (!CanAddItems(1))
            throw new Exception("can't add item");
        resourcesItems.Add(item);
        if (isStorage)
            ResourcesManager.instance.UpdateResourcesInfo();
        isFull = inventoryMaxCount <= resourcesItems.Count;
    }

    public void AddItems(ResourcesItem[] resources)
    {
        if (!CanAddItems(resources.Length))
            throw new Exception("can't add item");
        resourcesItems.AddRange(resources);
        if (isStorage)
            ResourcesManager.instance.UpdateResourcesInfo();
        isFull = inventoryMaxCount <= resourcesItems.Count;
    }

    public bool IsFull()
    {
        return isFull;
    }
    public bool CanAddItems(int itemsCount)
    {
        return inventoryMaxCount >= resourcesItems.Count + itemsCount;
    }
    public void ChangeMaxCount(int newMaxCount)
    {
        if (newMaxCount > 0)
            inventoryMaxCount = newMaxCount;
        if(resourcesItems!=null && resourcesItems.Count>0)
            isFull = inventoryMaxCount <= resourcesItems.Count;
    }
    public ResourcesItem[] GetAllResourcesOfType(ResourcesManager.ResourcesType resourcesType)
    {
        ResourcesItem[] resourcesItemsOfType = resourcesItems.Where(a => a.GetResourcesType() == resourcesType).ToArray();
        resourcesItems = resourcesItems.Except<ResourcesItem>(resourcesItemsOfType.ToList()).ToList();
        isFull = inventoryMaxCount <= resourcesItems.Count;
        if (isStorage)
            ResourcesManager.instance.UpdateResourcesInfo();
        return resourcesItemsOfType;
    }

    public ResourcesItem[] GetAllResources()
    {
        var tempList =  resourcesItems.ToArray();
        resourcesItems.Clear();
        isFull = inventoryMaxCount <= resourcesItems.Count;
        if (isStorage)
            ResourcesManager.instance.UpdateResourcesInfo();
        return tempList;
    }

    public int GetCountOfResourcesType(ResourcesManager.ResourcesType resourcesType)
    {
        return resourcesItems.Where(a => a.GetResourcesType() == resourcesType).ToArray().Length;
    }

}
