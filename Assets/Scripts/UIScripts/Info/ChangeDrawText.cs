using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeDrawText : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public IntVariableSO size;
    void Update()
    {
        numberText.text = size.Value.ToString();
    }
}