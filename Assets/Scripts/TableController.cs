using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : Singleton<TableController>
{
    public GameManagerSO gameManager;
    public int maxHandSize = 4;
    public CardSO[] drawDeck;
    public CardSO[] hand;
    public CardSO[] discardDeck;

    public void Start()
    {
        StartCycle(gameManager.cycle);
    }

    public void OnEnable()
    {
        gameManager.OnCycleStart.AddListener(StartCycle);
        gameManager.OnTurnStart.AddListener(StartTurn);
        gameManager.OnTurnEnd.AddListener(EndTurn);
        gameManager.OnCombatEnd.AddListener(EndCombat);
    }

    public void OnDisable()
    {
        gameManager.OnCycleStart.RemoveListener(StartCycle);
        gameManager.OnCombatEnd.RemoveListener(EndCombat);
        gameManager.OnTurnStart.RemoveListener(StartTurn);
        gameManager.OnTurnEnd.RemoveListener(EndTurn);
    }

    public void StartCycle(int cycle)
    {
        Debug.Log("Cycle " + cycle + " started");
        gameManager.StartCombat();
        gameManager.StartTurn();
    }

    public void StartTurn(int turn)
    {
        Debug.Log("Turn " + turn + " started");
    }

    public void EndTurn(int turn)
    {
        Debug.Log("Turn " + turn + " ended");
    }

    public void EndCombat()
    {
        Debug.Log("End of combat");
    }
}
