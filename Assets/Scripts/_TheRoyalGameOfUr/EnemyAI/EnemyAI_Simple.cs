using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Simple : MonoBehaviour {


    bool rolled;
    bool tokenSelected;

    GameFlowControl control;
    List<Token> enemyToken;
    List<Token> playerToken;
    List<int> playerTokenPos;
    DiceBox dicebox;

    private void Awake()
    {
        control = FindObjectOfType<GameFlowControl>();
    }
    private void Start()
    {
        dicebox = control.diceBox[1];
        enemyToken = control.enemyTokens;
    }

    private void Update()
    {
        if (control.playerTurn)
        {
            rolled = false;
            tokenSelected = false;
            return;
        }
        enemyToken = control.enemyTokens;
        if (dicebox.input.enabled&&!rolled)
        {
            dicebox.input.mouseUp = true;
            rolled = true;
            tokenSelected = false;
        }
        if (enemyToken.Exists(t => t.gameObject.GetComponent<InputManager>().enabled)&&!tokenSelected)
        {

            SelectToken().input.mouseUp = true;

            tokenSelected = true;
            rolled = false;
        }

    }
    Token SelectToken()
    {
        List<Token> canMoveTokens = enemyToken.FindAll(t => t.canMove);
        playerToken = control.playerTokens;
        playerTokenPos = new List<int>();
        foreach (Token token in playerToken.FindAll(t=>t.onBoard))
        {
            playerTokenPos.Add(token.currentTile);
        }
        
        if (!canMoveTokens.Exists(t => t.onBoard))
        {
            return canMoveTokens[0];
        }
        List<Token> onBoardTokens = canMoveTokens.FindAll(t => t.onBoard);
        List<Token> tokenOnHolder = new List<Token>();
        foreach (Token token in canMoveTokens)
        {
            if (!token.onBoard)
            {
                tokenOnHolder.Add(token);
            }
        }
        Debug.Log("OnHolder: " + tokenOnHolder.Count);
        Debug.Log("OnBoard: " + onBoardTokens.Count);
        if (canMoveTokens.Exists(t => t.currentTile+t.moves==3|| t.currentTile + t.moves == 7|| t.currentTile + t.moves == 13))
        {
            return canMoveTokens.Find(t => t.currentTile + t.moves == 3 || t.currentTile + t.moves == 7 || t.currentTile + t.moves == 13);
        }
        if (playerTokenPos.Count > 0)
        {
            foreach(int i in playerTokenPos)
            {
                if (canMoveTokens.Exists(t=> t.currentTile + t.moves == i))
                {
                    return canMoveTokens.Find(t => t.currentTile + t.moves == i);
                }
            }
        }
        if (onBoardTokens.Count < 2 && tokenOnHolder.Count > 0)
        {
            return tokenOnHolder[0];
        }
        if (onBoardTokens.Exists(t => t.currentTile > 11))
        {
            return onBoardTokens.Find(t => t.currentTile > 11);
        }
        if(onBoardTokens.Exists(t => t.currentTile > 7 && t.currentTile < 12))
        {
            return onBoardTokens.Find(t => t.currentTile > 7 && t.currentTile < 12);
        }
        if (onBoardTokens.Exists(t => t.currentTile>3&& t.currentTile < 7))
        {
            return onBoardTokens.Find(t => t.currentTile > 3 && t.currentTile < 7);
        }
        if (tokenOnHolder.Count > 0)
        {
            return tokenOnHolder[0];
        }
        if (onBoardTokens.Exists(t => t.currentTile <4))
        {
            return onBoardTokens.Find(t => t.currentTile <4);
        }
        return canMoveTokens.Find(t => t.canMove);
    }
}
