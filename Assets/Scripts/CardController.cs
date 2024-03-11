using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public CardSO cardData;

    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI mana;

    public void LoadData(CardSO cardData)
    {
        nameText.text = cardData.cardName;
        description.text = cardData.cardDescription;
        mana.text = cardData.cost.ToString();
        if (cardData.cardImage)
            image.sprite = cardData.cardImage;
        transform.localScale = new Vector3( 1, 1, 1);
    }

    public void PlayCard()
    {
        // Add to discard pile
        TableController.Instance.PlayInTable(gameObject);

        Debug.Log("Card played: " + cardData.name);


        // Execute all card actions
        // foreach (CardAction action in cardData.cardActions)
        // {
        //     if (action.executeIfStatusActive != null && action.executeIfStatusActive.Value <= 0)
        //         continue; // Skip action if status is selected and not active
        //     switch (action.actionType)
        //     {
        //         case CardAction.ActionType.damage:
        //             // Deal damage
        //             Debug.Log("Dealt " + action.amount.value + " damage");
        //             break;
        //         case CardAction.ActionType.heal:
        //             // Heal
        //             Debug.Log("Healed " + action.amount.value + " health");
        //             break;
        //         case CardAction.ActionType.block:
        //             // Block
        //             Debug.Log("Blocked " + action.amount.value + " damage");
        //             break;
        //         case CardAction.ActionType.drawRandomCard:
        //             // Draw random card
        //             Debug.Log("Drew " + action.amount.value + " cards");
        //             break;
        //         case CardAction.ActionType.recoverCard:
        //             // Recover card
        //             Debug.Log("Recovered " + action.amount.value + " cards");
        //             break;
        //         case CardAction.ActionType.discardRandomCard:
        //             // Discard random card
        //             Debug.Log("Discarded " + action.amount.value + " cards");
        //             break;
        //         case CardAction.ActionType.applyStatus:
        //             // Apply status
        //             Debug.Log("Applied status ");
        //             break;
        //     }
        // }

        // Deactivate card
        // DeactivateObject();

    }

    public void DeactivateObject()
    {
        //TableController.Instance.AddToDiscardPile(cardData);
        gameObject.SetActive(false);
    }

    // public void DealDamage(CardAction action)
    // {
    //     float baseDamage = action.amount.value;
    //     float playerDamageOutput;

    //     // Apply player status multipliers
    //     if (PlayerController.Instance.playerWeak.Value > 0 && PlayerController.Instance.playerFury.Value > 0)
    //     {
    //         playerDamageOutput = baseDamage;
    //     }
    //     else
    //     {
    //         if (PlayerController.Instance.playerWeak.Value > 0)
    //         {
    //             playerDamageOutput = baseDamage * 0.8f;
    //         }
    //         else if (PlayerController.Instance.playerFury.Value > 0)
    //         {
    //             playerDamageOutput = baseDamage * 1.2f;
    //         }
    //         else
    //         {
    //             playerDamageOutput = baseDamage;
    //         }
    //     }

    //     // Aplly boss status multipliers

    // }
}
