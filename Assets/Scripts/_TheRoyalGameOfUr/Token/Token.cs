using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {
    
    public int teamIndex;
    public int currentTile = -1;
    public int nextTile = 1;
    public int orderInHolder;

    public float moveThreshold = 0.1f;
    public float moveThresholdY = 0.001f;
    public float angleThreshold = 0.1f;
    public float moveSpeed = 5f;
    public float ysmooth = 0.03f;

    public bool onBoard;
    public bool isMoving;
    public bool isAttacking;
    public bool isTurning;
    public bool scored;
    public bool endPath = true;
    public bool canMove = true;

    public GameObject soul;

    public int moves;
    float x_0;
    

    [HideInInspector]
    public Vector3 targetPos;

    Quaternion targetRot;

    ScoreManager score;
    TileMap tileMap;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public List<Tile> completePath = new List<Tile>();
    [HideInInspector]
    public Queue<Tile> currentPath = new Queue<Tile>();
    [HideInInspector]
    public GameFlowControl control;
    TokenHolder holder;
    [HideInInspector]
    public Token selfRef;
    [HideInInspector]
    public InputManager input;

    public StateMachine stateMachine = new StateMachine(); 

    private void Start()
    {
        control = FindObjectOfType<GameFlowControl>();
        tileMap = FindObjectOfType<TileMap>();
        input = GetComponent<InputManager>();
        selfRef = GetComponent<Token>();
        anim = GetComponent<Animator>();
        score = FindObjectOfType<ScoreManager>();

        stateMachine.SetDefaultState(new IdleState());
        stateMachine.Default();

        targetRot = transform.rotation;
        FindTeamHolder(teamIndex);
        FindCompletePath(teamIndex);
        GoToHolder(orderInHolder);
        input.enabled = false;
    }

    //---STATE MACHINE----------------------------------------
    public void PathFinding(Trigger trigger)
    {
        moves = control.currentDiceCount;
        currentPath.Clear();
        if (moves == 0)
        {
            return;
        }
        for (int i = 0; i < moves; i++)
        {
            currentTile++;
            if (currentTile == completePath.Count)
            {
                Score();
                break;
            }
            else
            {
                currentPath.Enqueue(completePath[currentTile]);
            }
        }
        control.DeactivateTokens(selfRef);
        stateMachine.ChangeState(new Moving(this, currentPath));
        endPath = false;
    }
    public void DoneRotating()
    {
        stateMachine.ChangeState(new Moving(this, currentPath));
    }

    public void EndPath()
    {
        endPath = true;
        stateMachine.Default();
        if (completePath[currentTile].doubleTurn)
        {
            control.DoubleTurn();
        }
        else
        {
            control.ChangeTurn();
        }
    }

    //--------------------------------------------------------

    //---MOVEMENT MANAGER-------------------------------------   
    public void CheckRotation()
    {
        if (!onBoard)
            return;
        SetRotationTarget(completePath[nextTile].transform.position);

        stateMachine.ChangeState(new Turning(this, targetRot));
    }
    //--
    public void RotateTowards(Vector3 target)
    {
        SetRotationTarget(target);
        stateMachine.ChangeState(new Turning(this, targetRot));
    }
    //--
    private void SetRotationTarget(Vector3 targetPos)
    {
        var target = targetPos - transform.position;
        target = new Vector3(target.x, 0, target.z);

        targetRot = Quaternion.LookRotation(target, transform.up);
    }
    //--------------------------------------------------------

    //---COMBAT MECHANICS-------------------------------------
    public void AttackPhase()
    {
        stateMachine.ChangeState(new Attacking(this));
    }
    public void AttackEnd()
    {
        //stateMachine.ChangeState(new Moving(this, currentPath));
    }
    public void Die()
    {
        GoToHolder(orderInHolder);
    }
    private void Score()
    {
        stateMachine.Default();
        scored = true;
        Instantiate(soul, transform.position, transform.rotation);
        score.AddScore(teamIndex);
        control.ChangeTurn();
        control.TokenKilled(this);
    }
    //--------------------------------------------------------

    //---OTHER------------------------------------------------
    public void UpdateMap()
    {
        tileMap = FindObjectOfType<TileMap>();
    }
    //Start Methods (Called once)---------------------------------------
    void FindCompletePath(int index)
    {
        if (index == 0)
        {
            completePath = tileMap.playerPath;
        }
        else
        {
            completePath = tileMap.enemyPath;
        }
    }

    private void GoToHolder(int index)
    {
        targetPos = holder.slots[index].position;
        onBoard = false;
        currentTile = -1;
        nextTile = -1;
    }

    private void FindTeamHolder(int index)
    {
        TokenHolder[] allHolders = FindObjectsOfType<TokenHolder>();
        foreach (TokenHolder obj in allHolders)
        {
            if (obj.teamIndex == index)
            {
                holder = obj;
            }
        }
    }
//-----------------------------------------------------------------------------
}
