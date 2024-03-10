using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeDrawText : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    void Update()
    {
        numberText.text = "Size of Draw Array";
    }
}