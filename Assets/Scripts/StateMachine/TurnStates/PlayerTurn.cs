using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : IState {

    private System.Action<TurnManager> turnCheck;
    UIManager manager;

    public PlayerTurn(System.Action<TurnManager> turnCheck,UIManager manager)
    {
        this.turnCheck = turnCheck;
        this.manager = manager;
    }

	public void StartState()
    {
        Debug.Log("PlayerTurn");
        turnCheck(new TurnManager(true, false));
        manager.PlayerTurnTrigger();
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {

    }
}

public class TurnManager
{
    public bool playerTurn;
    public bool enemyTurn;

    public TurnManager(bool playerTurn, bool enemyTurn)
    {
        this.playerTurn = playerTurn;
        this.enemyTurn = enemyTurn;
    }
}
