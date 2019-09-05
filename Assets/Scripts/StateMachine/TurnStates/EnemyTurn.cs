using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : IState
{
    private System.Action<TurnManager> turnCheck;
    UIManager manager;

    public EnemyTurn(System.Action<TurnManager> turnCheck,UIManager manager)
    {
        this.turnCheck = turnCheck;
        this.manager = manager;
    }

    public void StartState()
    {
        Debug.Log("EnemyTurn");
        turnCheck(new TurnManager(false, true));
        manager.EnemyTurnTrigger();
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
    }
}