using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : IState {

    Token owner;

    public Attacking(Token owner)
    {
        this.owner = owner;
    }

    public void ExitState()
    {
        owner.isAttacking = false;
    }

    public void StartState()
    {
        owner.isAttacking = true;
    }

    public void UpdateState()
    {

    }
}
