using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnmatorController : MonoBehaviour
{
    public enum AnimatorState
    {
        Run,
        Die,
        Attack,
        Stay
    }

    public AnimatorState animatorState;

    private AnimatorState prevAnimatorState;
    private void Update()
    {
        if (animatorState != prevAnimatorState)
        {
            gameObject.GetComponent<Animator>().SetTrigger(animatorState.ToString());
            prevAnimatorState = animatorState;
        }
    }
}
