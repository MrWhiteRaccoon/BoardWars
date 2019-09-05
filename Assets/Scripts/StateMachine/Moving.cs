using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : IState {

    Token owner;
    Queue<Tile> currentPath;
    bool willAttack;

    float x_0=1f;

    public Moving(Token owner, Queue<Tile> currentPath)
    {
        this.owner = owner;
        this.currentPath = currentPath;
    }

    public void ExitState()
    {
        owner.isMoving = false;
    }

    public void StartState()
    {
        owner.isMoving = true;
    }

    public void UpdateState()
    {
        Move();
    }

    private void AddY()
    {
        float offset = 1f - Mathf.Pow(Mathf.Abs(owner.transform.position.x - owner.targetPos.x) / x_0 - 1f, 2);
        offset = offset * owner.ysmooth;
        owner.transform.position += new Vector3(0, offset, 0);
    }


    private void Move()
    {
        if ((owner.transform.position - owner.targetPos).magnitude > owner.moveThreshold)
        {
            owner.transform.position = Vector3.Lerp(owner.transform.position, owner.targetPos, Time.deltaTime * owner.moveSpeed);
            AddY();
        }
        else
        {
            SetNextTarget();
        }
    }
    private void SetNextTarget()
    {
        if (currentPath.Count == 0)
        {
            owner.EndPath();
            return;
        }
        else
        {
            WillAttack();
            if (willAttack)
            {
                owner.AttackPhase();
            }
            owner.targetPos = currentPath.Dequeue().transform.position;
            if(!willAttack)
            {
                owner.RotateTowards(owner.targetPos);
            }
            owner.nextTile++;
            owner.onBoard = true;
        }
    }
    private void WillAttack()
    {
        willAttack = (currentPath.Count == 1 && owner.completePath[owner.currentTile].tokenOnTop != null);
    }
}
