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
        if (cycleData.cycle > 0)
            cycleText.text = cycleData.cycle.ToString() + 1;
    }
}
