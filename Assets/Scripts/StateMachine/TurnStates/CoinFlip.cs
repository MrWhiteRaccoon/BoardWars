using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFlip : IState
{
    System.Action<TurnManager> coinFlipped;

    public CoinFlip(Action<TurnManager> coinFlipped)
    {
        this.coinFlipped = coinFlipped;
    }

    public void ExitState()
    {
    }

    public void StartState()
    {
        var playerTurn = false;
        var enemyTurn = false;
        if (UnityEngine.Random.value > 0.5f)
            playerTurn = true;
        else
            enemyTurn = true;

        coinFlipped(new TurnManager(playerTurn,enemyTurn));
    }

    public void UpdateState()
    {
    }
}

