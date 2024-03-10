using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements.Experimental;
using JetBrains.Annotations;

public class PlayerStatusScript : MonoBehaviour
{
    public GameObject BlockObj, FuryObj, ThornObj, WeakObj;
    public IntVariableSO dataBlock, dataFury, dataThorn, dataWeak;

    void Update()
    {
        ActiveObj(BlockObj);
        ActiveObj(FuryObj);
        ActiveObj(ThornObj);
        ActiveObj(WeakObj);
    }

    public void ActiveObj(GameObject o)
    {
        IntVariableSO data = ChooseData(o);
        if (data.Value >= 1)
        {
            o.SetActive(true);
            TextMeshProUGUI text = o.GetComponentInChildren<TextMeshProUGUI>();
            text.text = data.Value.ToString();
        }
        else
            o.SetActive(false);
    }

    IntVariableSO ChooseData(GameObject o)
    {
        if (o.name == "Block")
            return dataBlock;
        else if (o.name == "Fury")
            return dataFury;
        else if (o.name == "Thorn")
            return dataThorn;
        else if (o.name == "Weak")
            return dataWeak;
        return null;
    }
}
