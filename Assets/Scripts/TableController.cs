using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using TMPro;
using System;

public class TableController : Singleton<TableController>
{
    public GameManagerSO gameManager;
    public int maxHandSize = 4;
    public CardSO[] drawPile;
    public GameObject[] hand;
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

    public void DrawCard()
    {

        foreach (GameObject card in hand)
        {
            // Iteramos por las cartas
            if (!card.activeSelf)
            {
                Debug.Log("ESTOY INACTIVO Y ROBO");
                string tmp = drawPile[0].cardName;
                RemoveFirstElement();
            }
            // En la primera inactiva
                // En caso de que no haya cartas para robar ejecutamos la función ShuffleDeck
                // Obtenemos los datos de la carta a robar y la guardamos en una variable temporal
                // Quitamos la carta obtenida de la pila de robo
                // Activamos la carta
                // Cogemos el CardController de la carta y llamamos a LoadData con la card data robada.
        }
    }

    public void RemoveFirstElement()
    {
        if (drawPile.Length > 1)
        {
            CardSO[] newDrawPile = new CardSO[drawPileSize.Value - 1];
            Array.Copy(drawPile, 1, newDrawPile, 0, drawPile.Length - 1);
            drawPile = newDrawPile;
        }
        else
        {
            Debug.Log("PILA DE ROBO INACTIVA");
            drawPile = null;
        }
    }

    public void ShuffleDeck()
    {

    }

    public void AddToDiscardPile(CardSO card)
    {
        // Añades el card data a discard pile

    }

    public void DiscardCard(GameObject card)
    {
        // Desactivas la carta
    }

    public void DiscardRandomCard()
    {

    }

    public void CopyDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI text = hand[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = gameManager.playerDeck[i].cardName;
        }
    }

    void Start()
    {
        CopyDeck();
        DrawCard();
    }
}
