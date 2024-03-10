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
            image = cardData.cardImage;
        transform.localScale = new Vector3(1, 1, 1);
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
