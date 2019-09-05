using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBox : MonoBehaviour {

    public int count;
    public int teamIndex;
    public bool rolled;

    public RawImage diceView;
    public Text countText;

    bool waiting;

    List<DiceController> dice=new List<DiceController>();

    public InputManager input;
    public StateMachine stateMachine = new StateMachine();

    GameFlowControl control;

    private void Awake()
    {
        input = GetComponent<InputManager>();
        FindDice();
        control = FindObjectOfType<GameFlowControl>();
    }

    private void Start()
    {
        diceView.gameObject.SetActive(false);
        input.enabled = false;

        stateMachine.SetDefaultState(new IdleState());
        stateMachine.Default();
    }

    private void Update()
    {
        stateMachine.UpdateMachine();
        if (!rolled)
        {
            return;
        }
        if (!DiceRolling())
        {
            if(!waiting)
                StartCoroutine("CountWait");
        }
    }

    public void RollDice(Trigger trigger)
    {
        diceView.gameObject.SetActive(true);
        foreach (DiceController obj in dice)
        {
            obj.RollDice();
        }
        StartCoroutine("RolledWait");
    }
    bool DiceRolling()
    {
        foreach (DiceController obj in dice)
        {
            if (obj.isRolling)
            {
                StopAllCoroutines();
                waiting = false;
                return true;
            }
        }
        return false;
    }

    void CountDice()
    {
        count = 0;
        foreach (DiceController obj in dice)
        {
            if (obj.CheckScore())
            {
                count++;
            }
        }
        UpdateUI(count);
        StartCoroutine("CountImageWait");
    }


    void UpdateUI(int count)
    {
        countText.text = "" + count;
    }

    void FindDice()
    {
        foreach(DiceController dice in FindObjectsOfType<DiceController>())
        {
            if (dice.teamIndex == teamIndex)
            {
                this.dice.Add(dice);
            }
        }
    }

    IEnumerator CountWait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.5f);
        CountDice();
        

        rolled = false;
        waiting = false;
        stateMachine.Default();
        control.DiceRolled(count);

    }

    IEnumerator RolledWait()
    {
        yield return new WaitForSeconds(0.1f);
        rolled = true;
    }
    IEnumerator CountImageWait()
    {
        yield return new WaitForSeconds(0.5f);
        diceView.gameObject.SetActive(false);
    }
}
