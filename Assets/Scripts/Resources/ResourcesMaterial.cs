using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMaterial : MonoBehaviour
{
    [SerializeField]
    private ResourcesItem resource;
    [SerializeField] [Range(0,100)]
    private int resourcesCount;
    private float miningDifficulty;
    [SerializeField]
    private float currentMiningDifficulty;
    [SerializeField]
    private bool isMined;
    [SerializeField]
    private bool isFinished;
    [SerializeField]
    private bool useAnimator;
    [SerializeField]
    private float timeToDead;

    private Animator animator;

    private void Start()
    {
        if(resource != null)
            miningDifficulty = resource.GetMiningDifficulty();
        currentMiningDifficulty = miningDifficulty;
        if (useAnimator)
        {
            animator = animator == null ? gameObject.GetComponent<Animator>() : animator;
            //animator.SetTrigger("Idle");
        }
    }
    public bool CanExtractResources()
    {
        return !isMined && resourcesCount > 0;
    }
    public void ExtractResources(float _miningDifficulty)
    {
        if (currentMiningDifficulty <= 0)
        {
            isMined = true;
            return;
        }
        currentMiningDifficulty -= _miningDifficulty;
        currentMiningDifficulty = Mathf.Clamp(currentMiningDifficulty, 0, miningDifficulty);
    }
    public bool CanTakeResources()
    {
        return isMined;
    }

    public bool IsFinished()
    {
        return isFinished;
    }


    public ResourcesItem TakeResources()
    {
        if (!CanTakeResources())
            throw new PersonException("Can't take resources");
        isMined = false;
        resourcesCount--;
        isFinished = resourcesCount <= 0 && !isMined;
        if (isFinished)
            ResourcesFinished();
        currentMiningDifficulty = miningDifficulty;
        return resource;
    }

    private void ResourcesFinished()
    {
        if (useAnimator)
        {
            animator = animator == null ? gameObject.GetComponent<Animator>() : animator;
            animator.SetTrigger("Down");
        }
        Destroy(this.gameObject, timeToDead);
    }

}
