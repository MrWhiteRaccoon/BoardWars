using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : IState
{

    Token owner;
    Quaternion targetRot;

    public Turning(Token owner,Quaternion targetRot)
    {
        this.owner = owner;
        this.targetRot = targetRot;
    }

    public void ExitState()
    {
        owner.isTurning = false;
    }

    public void StartState()
    {
        owner.isTurning = true;
    }

    public void UpdateState()
    {
        if ((owner.transform.rotation.eulerAngles - targetRot.eulerAngles).magnitude > owner.angleThreshold)
        {
            owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, targetRot, Time.deltaTime * owner.moveSpeed);
        }
        else
        {
            owner.DoneRotating();
        }
    }
}
