using System.Collections;
using System.Collections.Generic;
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

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

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
        TableController.Instance.PlayInTable(gameObject);
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

}
