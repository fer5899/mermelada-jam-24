using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossStatusScript : MonoBehaviour
{
    public GameObject PoisonObj, BloodObj, WeakObj;
    public IntVariableSO dataPoison, dataBlood, dataWeak;

    void Update()
    {
        ActiveObj(PoisonObj);
        ActiveObj(BloodObj);
        ActiveObj(WeakObj);
    }

    public void ActiveObj(GameObject o)
    {
        IntVariableSO data = ChooseData(o);
        if (data.Value >= 1)
        {
            o.SetActive(true);
            TextMeshProUGUI text = o.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log("DATA VALUE " + data.Value);
            text.text = data.Value.ToString();
        }
        else
            o.SetActive(false);
    }

    IntVariableSO ChooseData(GameObject o)
    {
        if (o.name == "Poison")
            return dataPoison;
        else if (o.name == "Blood")
        {
            //Debug.Log("FGLASDBFLABSFLASBFLASBFLABSFLJABSLFJBASLJFDBLSAJB");
            return dataBlood;
        }
        else if (o.name == "Weak")
            return dataWeak;
        return null;
    }
}
