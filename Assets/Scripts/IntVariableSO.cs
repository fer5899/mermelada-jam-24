using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntVariable", menuName = "IntVariable")]
public class IntVariableSO : ScriptableObject
{
    public int value;
    public int defaultValue;

    [System.NonSerialized]
    public UnityEvent<int> OnValueChanged;

    public void OnEnable()
    {
        value = defaultValue;
        OnValueChanged ??= new UnityEvent<int>();
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        OnValueChanged.Invoke(value);
    }

    public void ChangeValue(int amount)
    {
        value += amount;
        OnValueChanged.Invoke(value);
    }

    public void ResetValue()
    {
        value = defaultValue;
    }
}
