using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaScript : MonoBehaviour
{
    public TextMeshProUGUI manaText;
    public IntVariableSO manaData;
    
    void Update()
    {
        manaText.text = manaData.Value.ToString() + "/" + manaData.DefaultValue.ToString();
    }
}
