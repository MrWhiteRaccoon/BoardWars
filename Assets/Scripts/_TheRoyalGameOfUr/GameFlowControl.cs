using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowControl : MonoBehaviour {

    public bool playerTurn;
    public bool enemyTurn;
    public GameObject menu;

    public int currentDiceCount;
    public float thresholdDist = 0.1f;

    public DiceBox[] diceBox;
    
    public StateMachine stateMachine = new StateMachine();

    public List<Token> playerTokens = new List<Token>();
    public List<Token> enemyTokens = new List<Token>();
    UIManager manager;
    public List<Token> tokenOnBoard = new List<Token>();

    private void Start()
    {
        SortSceneTokens();
        GetBoxes();
        manager = GetComponent<UIManager>();

        stateMachine.ChangeState(new CoinFlip(CoinFlipDone));
        stateMachine.SetDefaultState(new IdleState());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }

    //---TURN MANAGEMENT---------------------------
    private void CoinFlipDone(TurnManager turnManager)
    {
        if (turnManager.playerTurn)
            stateMachine.SwitchTurn(new PlayerTurn(TurnCheck,manager));
        else
            stateMachine.SwitchTurn(new EnemyTurn(TurnCheck,manager));
        stateMachine.Default();
    }
    private void TurnCheck(TurnManager turnManager)
    {
        playerTurn = turnManager.playerTurn;
        enemyTurn = turnManager.enemyTurn;
        if (playerTurn)
        {
            diceBox[0].stateMachine.ChangeState(new WaitForTrigger(diceBox[0].input, diceBox[0].RollDice));
        }
        else
        {
            diceBox[1].stateMachine.ChangeState(new WaitForTrigger(diceBox[1].input, diceBox[1].RollDice));
        }
    }
    public void ChangeTurn()
    {
        if (playerTurn)
        {
            stateMachine.SwitchTurn(new EnemyTurn(TurnCheck,manager));
            manager.EnemyTurnTrigger();
        }
        else
        {
            stateMachine.SwitchTurn(new PlayerTurn(TurnCheck, manager));
            manager.PlayerTurnTrigger();
        }
        TokensOnBoard();
    }
    public void DoubleTurn()
    {
        if (playerTurn)
        {
            stateMachine.SwitchTurn(new PlayerTurn(TurnCheck, manager));
        }
        else
        {
            stateMachine.SwitchTurn(new EnemyTurn(TurnCheck, manager));
        }
        TokensOnBoard();
    }
    //---------------------------------------------

    //---TOKEN MANAGEMENT--------------------------
    private void ActivateTokens(List<Token> tokenCanMove)
    {
        if (playerTurn)
        {
            if (!tokenCanMove.Exists(t => t.teamIndex == 0))
            {
                ChangeTurn();
            }
            foreach (Token token in tokenCanMove.FindAll(t=>t.teamIndex==0))
            {
                token.stateMachine.ChangeState(new WaitForTrigger(token.input, token.PathFinding));
            }
        }
        else
        {
            if (!tokenCanMove.Exists(t => t.teamIndex == 1))
            {
                ChangeTurn();
            }
            foreach (Token token in tokenCanMove.FindAll(t => t.teamIndex == 1))
            {
                token.stateMachine.ChangeState(new WaitForTrigger(token.input, token.PathFinding));
            }
        }
    }
    public void SortSceneTokens()
    {
        Token[] sceneTokens = FindObjectsOfType<Token>();
        playerTokens.Clear();
        enemyTokens.Clear();
        foreach (Token token in sceneTokens)
        {
            if (token.teamIndex == 0)
            {
                playerTokens.Add(token);
            }
            else
            {
                enemyTokens.Add(token);
            }
        }
    }
    public void DeactivateTokens(Token exception)
    {
        foreach (Token token in FindObjectsOfType<Token>())
        {
            if (token != exception)
            {
                token.stateMachine.Default();
            }
        }
    }
    public void TokensOnBoard()
    {
        tokenOnBoard.Clear();
        tokenOnBoard.AddRange(playerTokens.FindAll(t => t.onBoard));
        tokenOnBoard.AddRange(enemyTokens.FindAll(t => t.onBoard));

        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            foreach(Token token in tokenOnBoard)
            {
                if (token == null)
                {
                    continue;
                }
                if ((tile.transform.position - token.transform.position).magnitude < thresholdDist)
                {
                    tile.tokenOnTop = token;
                    break;
                }
                tile.tokenOnTop = null;
            }
        }
    }
    public void TokenKilled(Token token)
    {
        if (playerTokens.Find(t => t == token))
        {
            playerTokens.Remove(token);
        }
        else
        {
            enemyTokens.Remove(token);
        }
        Destroy(token.gameObject);
    }

    public List<Token> CheckAvailableTokens(int moves)
    {
        List<Token> tokenCanMove = new List<Token>();
        tokenCanMove.Clear();
        foreach(Token token in FindObjectsOfType<Token>())
        {
            token.canMove = true;
            int tokenPos = token.currentTile + moves;
            if (tokenPos > token.completePath.Count)
            {
                token.canMove = false;
            }
            else if(tokenPos <= token.completePath.Count-1 &&
                token.completePath[tokenPos].tokenOnTop!=null)
            {
                if (token.completePath[tokenPos].tokenOnTop.teamIndex == token.teamIndex
                    || token.completePath[tokenPos].safe)
                {
                    token.canMove = false;
                }
            }
            if (token.canMove)
            {
                tokenCanMove.Add(token);
            }
        }
        return tokenCanMove;
    }

    //---------------------------------------------

    //---DICE MANAGEMENT---------------------------
    public void DiceRolled(int rollCount)
    {
        currentDiceCount = rollCount;
        if (currentDiceCount == 0)
        {
            ChangeTurn();
            return;
        }
        ActivateTokens(CheckAvailableTokens(currentDiceCount));
    }
    private int RandomDice()
    {
        int res=0;
        for (int i = 0; i < 4; i++)
        {
            int d = Random.Range(0, 2);
            res += d;
        }
        return res;
    }
    private void GetBoxes()
    {
        diceBox = new DiceBox[2];
        foreach(DiceBox box in FindObjectsOfType<DiceBox>())
        {
            diceBox[box.teamIndex] = box;
        }
    }
    //---------------------------------------------
    
    







}
