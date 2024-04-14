using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public GameManagerSO gameManager;
    public CardSO cardData;

    public bool costsZeroMana = false;

    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI mana;
    private CanvasGroup canvasGroup;

    public void OnEnable()
    {
        gameManager.OnTurnStart.AddListener(OnTurnStart);
        gameManager.OnPlayerDeath.AddListener(DisableInteractivity);
        gameManager.OnBossDeath.AddListener(DisableInteractivity);
    }

    public void OnDisable()
    {
        gameManager.OnTurnStart.RemoveListener(OnTurnStart);
        gameManager.OnPlayerDeath.RemoveListener(DisableInteractivity);
        gameManager.OnBossDeath.RemoveListener(DisableInteractivity);
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
        TableController.Instance.PlayInTable(gameObject, costsZeroMana);
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    public void Start()
    {
        costsZeroMana = false;
        GetComponent<Button>().interactable = true;
    }

    public void Update()
    {
        if (costsZeroMana)
        {
            mana.text = "0";
        }
        else
        {
            mana.text = cardData.cost.ToString();
        }
    }

    public void OnTurnStart(int turn)
    {
        costsZeroMana = false;
    }

    public void DisableInteractivity()
    {
        GetComponent<Button>().interactable = false;
    }
}
