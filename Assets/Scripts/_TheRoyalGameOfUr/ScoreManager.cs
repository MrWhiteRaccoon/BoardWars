using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int[] score;
    public int maxScore;

    public Animator playerUI;
    public Animator enemyUI;

    GameFlowControl control;
    GameSwitcher switcher;

    private void Start()
    {
        score = new int[2];
        score[0]=score[1]=0;
        control = FindObjectOfType<GameFlowControl>();
        switcher = FindObjectOfType<GameSwitcher>();
    }

    public void AddScore(int index)
    {
        score[index]++;
        if (score[index] == maxScore)
        {
            if (index == 0)
            {
                playerUI.SetTrigger("PlayerWin");
            }
            else
            {
                enemyUI.SetTrigger("EnemyWin");
            }
            EndGame();
        }
    }

    public void EndGame()
    {
        control.stateMachine.SwitchTurn(new IdleState());
        switcher.EndCurrentGame();
    }
}
