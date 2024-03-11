using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.Common;

public class ChangeCycleImg : MonoBehaviour
{
    public TextMeshProUGUI cycleText;
    public GameManagerSO cycleData;
    void Update()
    {
        int cycleDisplay = cycleData.cycle + 1;
        cycleText.text = cycleDisplay.ToString();
    }
}
