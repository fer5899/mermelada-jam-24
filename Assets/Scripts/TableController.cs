using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : Singleton<TableController>
{
    public GameManagerSO gameManager;
    public int maxHandSize = 4;
    public CardSO[] drawPile;
    public CardSO[] hand;
    public CardSO[] discardPile;
    public IntVariableSO discardPileSize, drawPileSize;

    public void OnEnable()
    {
        gameManager.OnCycleStart.AddListener(StartCycle);
        gameManager.OnCycleEnd.AddListener(EndCycle);
        gameManager.OnTurnStart.AddListener(StartTurn);
        gameManager.OnTurnEnd.AddListener(EndTurn);
    }

    public void OnDisable()
    {
        gameManager.OnCycleStart.RemoveListener(StartCycle);
        gameManager.OnCycleEnd.RemoveListener(EndCycle);
        gameManager.OnTurnStart.RemoveListener(StartTurn);
        gameManager.OnTurnEnd.RemoveListener(EndTurn);
    }

    public void StartCycle(int cycle)
    {
        Debug.Log("Cycle " + cycle + " started");
    }

    public void EndCycle(int cycle)
    {
        Debug.Log("Cycle " + cycle + " ended");
    }

    public void StartTurn(int turn)
    {
        Debug.Log("Turn " + turn + " started");
    }

    public void EndTurn(int turn)
    {
        Debug.Log("Turn " + turn + " ended");
    }

    void Update ()
    {
        drawPileSize.SetValue(drawPile.Length);
        discardPileSize.SetValue(discardPile.Length);
    }
}
