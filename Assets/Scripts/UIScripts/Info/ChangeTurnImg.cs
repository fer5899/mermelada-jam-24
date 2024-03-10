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
        turnText.text = "Turno " + (turnData.turn.ToString() + 1); //+1 display
    }
}
