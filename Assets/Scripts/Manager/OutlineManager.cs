using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System;

public class OutlineManager : MonoBehaviour
{
    public static OutlineManager instance = null;
    [SerializeField]
    private OutlineEffect outline;
    private List<Outline> outlines = new List<Outline>();
    [SerializeField]
    private float maxBrightnes = 1f;

    private void Start()
    {
        outline.enabled = true;
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }

    private void InitializeManager()
    {
        Outline[] tempArray = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline outline in tempArray)
        {
            outlines.Add(outline);
            outline.enabled = false;
        }
    }

    public void ActiveOutline(Outline outline , bool isActive , int color  = 0)
    {
        if (outlines == null || outlines.Count < 1)
            throw new PersonException("Otlines == null Or empty");
        Outline currentOutline = outlines.Find(a => a == outline);
        currentOutline.enabled = isActive;
        if (isActive)
            currentOutline.color = color;
    }

    public void AddOutline(Outline newOutline)
    {
        if (outlines == null)
            throw new PersonException("Otlines == null");
        if (outlines.Find(a => a == newOutline) == null)
            //newOutline.
            outlines.Add(newOutline);
        else
            throw new PersonException("current Outline ( " + newOutline.gameObject.name + " ) already added");
    }

    public void RemoveOutline(Outline removedOutline)
    {
        if (outlines == null || outlines.Count < 1)
            throw new PersonException("Otlines == null Or empty");
        Outline currentOutline = outlines.Find(a => a == removedOutline);
        if (currentOutline==null)
            throw new PersonException("current Outline ( " + removedOutline.gameObject.name + " ) already deleted");
        outlines.Remove(removedOutline);

    }

}
