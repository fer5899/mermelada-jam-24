using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardAction
{
    // Action type combo box
    public enum ActionType
    {
        damage,
        heal,
        block,
        drawRandomCard,
        recoverCard,
        discardRandomCard,
        applyStatus,
    }


    public ActionType actionType;
    public IntVariableSO executeIfStatusActive;


    [Space]

    public IntReference amount;
    public IntReference repetition;

    [Space]

    public bool newCardsCostZero = false;

    [Space]
    public IntVariableSO targetStatus;

}
