using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class CardSO : ScriptableObject
{
    [Header("UI data")]
    public string cardName;
    public string cardDescription;
    public Image cardImage;
    public int cost;

    public List<CardAction> cardActions;

}
