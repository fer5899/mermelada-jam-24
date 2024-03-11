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
        gameManager.OnTurnStart.AddListener(StartTurn);
        gameManager.OnTurnEnd.AddListener(EndTurn);
    }

    public void OnDisable()
    {
        gameManager.OnCycleStart.RemoveListener(StartCycle);
        gameManager.OnTurnStart.RemoveListener(StartTurn);
        gameManager.OnTurnEnd.RemoveListener(EndTurn);
    }

    public void Start()
    {
        LoadHand();
        gameManager.StartCycle();
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

    public void DrawCard(bool costsZero = false)
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
                if (costsZero)
                {
                    drawnCard.cost = 0;
                }
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
        //DebugList(drawPile);
        ShuffleDeck(drawPile);
        //DebugList(drawPile);
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
        int aux = i;
        while (!hand[i].activeSelf)
        {
            i++;
            if (i >= hand.Length)
                i = 0;
            if (i == aux)
                return;
        }
        DiscardCard(hand[i]);
    }

    public void LoadHand()
    {
        drawPile = new List<CardSO>();

        for (int i = 0; i < gameManager.playerDeck.Length; i++)
        {
            drawPile.Add(gameManager.playerDeck[i]);
        }
        ShuffleDeck(drawPile);
    }

    public void PlayInTable(GameObject card)
    {
        CardSO cardData = card.GetComponent<CardController>().cardData;
        if (PlayerController.Instance.playerMana.Value < cardData.cost)
            return;
        DiscardCard(card);
        ExecuteCardActions(cardData);
        PlayerController.Instance.LoseMana(cardData.cost);
    }

    public void ExecuteCardActions(CardSO cardData)
    {
        foreach (CardAction action in cardData.cardActions)
        {
            if (action.executeIfStatusActive != null && action.executeIfStatusActive.Value <= 0)
                continue; // Skip action if status is selected and not active
            switch (action.actionType)
            {
                case CardAction.ActionType.damage:
                    DealDamage(action);
                    break;
                case CardAction.ActionType.heal:
                    Heal(action);
                    break;
                case CardAction.ActionType.block:
                    Block(action);
                    break;
                case CardAction.ActionType.drawRandomCard:
                    DrawCardsAction(action);
                    break;
                case CardAction.ActionType.discardRandomCard:
                    DiscardCardsAction(action);
                    break;
                case CardAction.ActionType.applyStatus:
                    ApplyStatus(action);
                    break;
            }
        }
    }

    public void DealDamage(CardAction action)
    {
        float baseDamage = action.amount.value;
        float playerDamageOutput;
        float bossTakenDamage;

        // Apply player status multipliers
        if (PlayerController.Instance.playerWeakStatusCounter.Value > 0 && PlayerController.Instance.playerFuryStatusCounter.Value > 0)
        {
            // If both weak and fury are active, they cancel each other out
            playerDamageOutput = baseDamage;
        }
        else
        {
            // If only one of them is active, apply the multiplier to the base damage
            if (PlayerController.Instance.playerWeakStatusCounter.Value > 0)
            {
                playerDamageOutput = baseDamage * 0.8f; // Weak multiplier
            }
            else if (PlayerController.Instance.playerFuryStatusCounter.Value > 0)
            {
                playerDamageOutput = baseDamage * 1.2f; // Fury multiplier
            }
            else
            {
                playerDamageOutput = baseDamage;
            }
        }

        bossTakenDamage = playerDamageOutput;
        // Aplly boss bleed if active
        if (BossController.Instance.bossBleedStatusCounter.Value > 0)
        {
            bossTakenDamage = playerDamageOutput * 1.2f; // Bleed multiplier
        }

        BossController.Instance.TakeDamage((int)Mathf.Ceil(bossTakenDamage));

        // Repeat damage dealing as many times as repetition value
        if (action.repetition.value > 0)
        {
            for (int i = 0; i < action.repetition.value; i++)
            {
                BossController.Instance.TakeDamage((int)Mathf.Ceil(bossTakenDamage));
            }
        }
    }

    public void Heal(CardAction action)
    {
        PlayerController.Instance.Heal(action.amount.value);

        // Repeat healing as many times as repetition value
        if (action.repetition.value > 0)
        {
            for (int i = 0; i < action.repetition.value; i++)
            {
                PlayerController.Instance.Heal(action.amount.value);
            }
        }
    }

    public void Block(CardAction action)
    {
        PlayerController.Instance.GainBlock(action.amount.value);

        // Repeat blocking as many times as repetition value
        if (action.repetition.value > 0)
        {
            for (int i = 0; i < action.repetition.value; i++)
            {
                PlayerController.Instance.GainBlock(action.amount.value);
            }
        }
    }

    public void ApplyStatus(CardAction action)
    {
        // Apply status
        if (action.targetStatus != null)
        {
            action.targetStatus.AddAmount(action.amount.value);
        }
    }

    public void DrawCardsAction(CardAction action)
    {
        for (int i = 0; i < action.amount.value; i++)
        {
            DrawCard(action.newCardsCostZero);
        }
    }

    public void DiscardCardsAction(CardAction action)
    {
        for (int i = 0; i < action.amount.value; i++)
        {
            DiscardRandomCard();
        }
    }

}
