using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine  {

    private IState currentState;
    private IState currentTurn;
    private IState defaultState;
    private IState previousState;

	public void ChangeState(IState newState)
    {
        if(currentState!=null)
            currentState.ExitState();
        previousState = currentState;
        currentState = newState;
        newState.StartState();
    }

    public void UpdateMachine()
    {
        var runningState = currentState;
        if (runningState != null)
            runningState.UpdateState();
    }

    public void PreviousState()
    {
        currentState.ExitState();
        currentState = previousState;
        currentState.StartState();
    }

    public void Default()
    {
        if (defaultState!=null)
            ChangeState(defaultState);
    }

    public void SetDefaultState(IState defaultState)
    {
        this.defaultState = defaultState;
    }

    public void SwitchTurn(IState turn)
    {
        currentTurn = turn;
        currentTurn.StartState();
    }
}
