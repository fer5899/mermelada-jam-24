using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class CardLoader : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI mana;

    public Image background;

    public CardSO cardData;

    private bool selected;

    public void LoadCard(CardSO cardData)
    {
        this.cardData = cardData;
        nameText.text = cardData.cardName;
        description.text = cardData.cardDescription;
        mana.text = cardData.cost.ToString();
        if (cardData.cardImage)
            image.sprite = cardData.cardImage;
    }

    public void SetCardAsSelected()
    {
        background.color = Color.yellow;
        selected = true;
    }

    public void SetCardAsDeselected()
    {
        background.color = Color.white;
        selected = false;
    }

    public void SelectDeckCard()
    {
        if (!selected)
        {
            UpgradeManager.Instance.SetAllDeckAsDeselected();
            SetCardAsSelected();
            UpgradeManager.Instance.SetSelectedDeckCard(cardData);
        }
        else
        {
            SetCardAsDeselected();
            UpgradeManager.Instance.SetSelectedDeckCard(null);
        }
    }

    public void SelectUpgradeCard()
    {
        if (!selected)
        {
            UpgradeManager.Instance.SetAllUpgradeAsDeselected();
            SetCardAsSelected();
            UpgradeManager.Instance.SetSelectedUpgradeCard(cardData);
        }
        else
        {
            SetCardAsDeselected();
            UpgradeManager.Instance.SetSelectedUpgradeCard(null);
        }
    }
    
}
