using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasaDamageableClass : HumanClass
{
    protected float rechargeTime;
    private float myRechargeTime;
    protected float classDamage;
    protected bool isRecharged;

    private void FixedUpdate()
    {
        if (!isRecharged)
        {

        }   
    }

    private void Recharge()
    {
        myRechargeTime -= Time.deltaTime;
        if (myRechargeTime <= 0)
        {
            isRecharged = true;
            myRechargeTime = rechargeTime;
        }
    }
}
