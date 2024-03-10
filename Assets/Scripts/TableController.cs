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
    private List<CardSO> drawPile;
    public GameObject[] hand;
    public List<CardSO> discardPile;
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
        DrawCard();
    }

    public void EndTurn(int turn)
    {
        Debug.Log("Turn " + turn + " ended");
        StartTurn(turn);
    }

    void Update ()
    {
        drawPileSize.SetValue(drawPile.Count);
        discardPileSize.SetValue(discardPile.Count);
    }

    public void DrawCard()
    {

        // Iteramos por las cartas
            // En caso de que no haya cartas para robar ejecutamos la función ShuffleDeck
            // En la primera inactiva
                // Obtenemos los datos de la carta a robar y la guardamos en una variable temporal
                // Quitamos la carta obtenida de la pila de robo
                // Cogemos el CardController de la carta y llamamos a LoadData con la card data robada.
        if (drawPile.Count <= 0)
            {
                Debug.Log("PILA DE ROBO VACIA");
                MoveCards();
            }
        foreach (GameObject card in hand)
        {
            
            Debug.Log("drawPileSize.Value " + drawPileSize.Value + "count " + drawPile.Count);

            if (!card.activeSelf)
            {
                CardSO drawnCard = drawPile[0];
                Debug.Log("drawnCard: " + drawnCard.cardName);
                drawPile.Remove(drawnCard);
                card.SetActive(true);
                CardController loadCard = card.GetComponent<CardController>();
                loadCard.cardData = drawnCard;
                loadCard.LoadData(drawnCard);
            }
        }
    }

    public void MoveCards()
    {
        for (int i = 0; i < discardPile.Count; i++)
            drawPile.Add(discardPile[i]);
        discardPile.Clear();
        Debug.Log("discardDeck: " + discardPile.Count);
    }
    public void ShuffleDeck()
    {

    }

    public void AddToDiscardPile(CardSO card)
    {
        discardPile.Add(card);
        Debug.Log("primer nodo lista descarte: " + discardPile[0].cardName);
    }

    public void DiscardCard(GameObject card)
    {
        // Desactivas la carta
    }

    public void DiscardRandomCard()
    {

    }

    public void LoadHand()
    {
        drawPile = new List<CardSO>();

        for (int i = 0; i < gameManager.playerDeck.Length; i++)
        {
            drawPile.Add(gameManager.playerDeck[i]);
        }
    }

    void Start()
    {
        LoadHand();
        StartTurn(gameManager.turn);
    }
}
