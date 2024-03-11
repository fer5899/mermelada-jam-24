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

        // Iteramos por las cartas
            // En caso de que no haya cartas para robar ejecutamos la función ShuffleDeck
            // En la primera inactiva
                // Obtenemos los datos de la carta a robar y la guardamos en una variable temporal
                // Quitamos la carta obtenida de la pila de robo
                // Cogemos el CardController de la carta y llamamos a LoadData con la card data robada.
    public void DrawCard()
    {
        if (drawPile.Count <= 0)
            {
                Debug.Log("PILA DE ROBO VACIA");
                MoveCards();
            }
        foreach (GameObject card in hand)
        {
            if (!card.activeSelf)
            {
                CardSO drawnCard = drawPile[0];
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
        Debug.Log("**------ESTAMOS EN MOVE CARDS ----------------**");
        DebugList(drawPile);
        ShuffleDeck(drawPile);
        DebugList(drawPile);
        discardPile.Clear();
    }
    void ShuffleDeck(List<CardSO> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();

        for (int i = n - 1; i > 0; i--)
        {
            // generar indice aleatorio entre 0 e i
            int k = rng.Next(0, i + 1);

            // swap
            CardSO temp = list[i];
            list[i] = list[k];
            list[k] = temp;
        }

    }

    public void DebugList(List<CardSO> list)
    {
        for (int i = 0; i < list.Count; i++)
            Debug.Log("list[" + i + "]: " + list[i].cardName);
        Debug.Log("----------------------");
    }
    public void AddToDiscardPile(CardSO card)
    {
        discardPile.Add(card);
    }

    public void DiscardCard(GameObject card)
    {
       AddToDiscardPile(card.GetComponent<CardController>().cardData);
       card.SetActive(false);
    }

    public void DiscardRandomCard()
    {
        System.Random rng = new System.Random();
        int i = rng.Next(0, hand.Length);
        Debug.Log("*** Discard RAndom card randomNb: ***" + i);
        DiscardCard(hand[i]);
    }

    public void LoadHand()
    {
        drawPile = new List<CardSO>();

        for (int i = 0; i < gameManager.playerDeck.Length; i++)
        {
            drawPile.Add(gameManager.playerDeck[i]);
        }
        DebugList(drawPile);
        ShuffleDeck(drawPile);
        DebugList(drawPile);
    }

    void Start()
    {
        LoadHand();
        StartTurn(gameManager.turn);
    }

    public void PlayInTable(GameObject card)
    {
        CardSO cardData = card.GetComponent<CardController>().cardData;
        DiscardCard(card);
    }
}
