using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardLoader : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI mana;

    public void LoadCard(CardSO cardData)
    {
        nameText.text = cardData.cardName;
        description.text = cardData.cardDescription;
        mana.text = cardData.cost.ToString();
        if (cardData.cardImage)
            image = cardData.cardImage;
    }
    
}
