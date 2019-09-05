using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public Animator playerTurn;
    public Animator enemyTurn;


    public void PlayerTurnTrigger()
    {
        playerTurn.SetTrigger("PlayerTurn");
    }

    public void EnemyTurnTrigger()
    {
        enemyTurn.SetTrigger("EnemyTurn");
    }
}
