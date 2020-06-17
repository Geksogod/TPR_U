using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ResourcesdData", menuName = "ScriptableObject/Logo", order = 1)]
public class FractionItem : ScriptableObject
{
    [SerializeField]
    private string fractionName;
    [SerializeField]
    private LogoManager.LogoColor logoColor;
    [SerializeField]
    private Sprite logoSprite;
    [SerializeField]
    private Material logoMaterial;
    public string playerName;
    public string playerNickName;
    public string playerCharacter;

    public Material GetMaterial()
    {
        return logoMaterial;
    }
    public Sprite GetSprite()
    {
        return logoSprite;
    }
    public LogoManager.LogoColor GetColor()
    {
        return logoColor;
    }
    public string GetName()
    {
        return fractionName;
    }
}
