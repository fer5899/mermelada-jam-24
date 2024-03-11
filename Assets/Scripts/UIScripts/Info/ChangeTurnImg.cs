using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTurnImg : MonoBehaviour
{
    public TextMeshProUGUI turnText;
    public GameManagerSO turnData;
    void Update()
    {
        int turnDisplay = turnData.turn + 1;
        turnText.text = "Turno " + turnDisplay.ToString();
    }
}
