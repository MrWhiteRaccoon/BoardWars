using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTrigger : IState
{
    private InputManager ownerInput;
    private System.Action<Trigger> trigger;

    public WaitForTrigger(InputManager ownerInput, Action<Trigger> trigger)
    {
        this.ownerInput = ownerInput;
        this.trigger = trigger;
    }

    public void StartState()
    {
        ownerInput.enabled = true;
       // Debug.Break();
    }
    public void ExitState()
    {
        ownerInput.enabled = false;
    }
    public void UpdateState()
    {
        if (ownerInput.mouseUp)
        {
            trigger(new Trigger());    // performs some action in the owner
            ownerInput.mouseUp = false;
        }
    }
}

public class Trigger
{

}
