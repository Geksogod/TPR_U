using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesMonitorUI : MonoBehaviour
{
    [SerializeField]
    private ResourcesManager.ResourcesType resourcesType;
    [SerializeField]
    private Sprite resourcesImage;
    [SerializeField]
    private int resourcesCount;
    [SerializeField]
    private TextMeshProUGUI textMesh;
    [SerializeField]
    private Image resourcesImageUI;

    private delegate void ChangeValue();

    private ChangeValue changeValue;

    private void Start()
    {
        resourcesImageUI.sprite = resourcesImage;
        ChangeUI();
        changeValue += ChangeUI;
    }

    public void AddResourcesValue(int _resourcesShift)
    {
        resourcesCount += _resourcesShift;
        changeValue.Invoke();
    }

    public void ChangeResourcesValue(int _resourcesCount)
    {
        resourcesCount = _resourcesCount;
        changeValue.Invoke();
    }

    public int GetResourcesCount()
    {
        return resourcesCount;
    }

    public ResourcesManager.ResourcesType GetResourcesMonitorType()
    {
        return resourcesType;
    }

    private void ChangeUI()
    {
        textMesh.text = resourcesCount.ToString();
    }

}
