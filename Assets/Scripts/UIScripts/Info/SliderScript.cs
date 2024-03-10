using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public IntVariableSO HealthData;
    public Image fillImg;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = HealthData.Value.ToString() + "/" + HealthData.DefaultValue.ToString();
        fillImg.fillAmount = (float)HealthData.Value / (float)HealthData.DefaultValue;
    }
}
