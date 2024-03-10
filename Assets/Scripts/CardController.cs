using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardSO cardData;

    public void PlayCard()
    {
        // Execute all card actions
        foreach (CardAction action in cardData.cardActions)
        {
            if (action.executeIfStatusActive != null && action.executeIfStatusActive.Value <= 0)
                continue; // Skip action if status is selected and not active
            switch (action.actionType)
            {
                case CardAction.ActionType.damage:
                    // Deal damage
                    Debug.Log("Dealt " + action.amount.value + " damage");
                    break;
                case CardAction.ActionType.heal:
                    // Heal
                    Debug.Log("Healed " + action.amount.value + " health");
                    break;
                case CardAction.ActionType.block:
                    // Block
                    Debug.Log("Blocked " + action.amount.value + " damage");
                    break;
                case CardAction.ActionType.drawRandomCard:
                    // Draw random card
                    Debug.Log("Drew " + action.amount.value + " cards");
                    break;
                case CardAction.ActionType.recoverCard:
                    // Recover card
                    Debug.Log("Recovered " + action.amount.value + " cards");
                    break;
                case CardAction.ActionType.discardRandomCard:
                    // Discard random card
                    Debug.Log("Discarded " + action.amount.value + " cards");
                    break;
                case CardAction.ActionType.applyStatus:
                    // Apply status
                    Debug.Log("Applied status ");
                    break;
            }
        }

        // Discard card
        Debug.Log("Card Discarded");
    }
}
