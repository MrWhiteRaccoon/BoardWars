using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoldierToken : Token {

    bool attacked = false;
    Token attackTarget;
    Token target;

    private void Update()
    {
        if (endPath)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
        if (isAttacking&&!attacked)
        {
            Debug.Log("Can Attack");
            if (FindTarget() != null)
            {
                target = FindTarget();
                RotateTowards(target.transform.position);
                Attack(attackTarget=target);
            }
            else
            {
                AttackEnd();
            }
        }
        stateMachine.UpdateMachine();
    }
    private Token FindTarget()
    {
        if (currentTile>0&&completePath[currentTile].tokenOnTop != null)
        {
            return completePath[currentTile].tokenOnTop;
        }
        else
        { return null; }
    }
    private void Attack(Token target)
    {
        anim.SetTrigger("Attack");
        attacked = true;
    }
    public void Hit()
    {
        attackTarget.Die();
        attackTarget = null;
        attacked = false;
        AttackEnd();
    }

}
