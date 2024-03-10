using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "IntVariable", menuName = "IntVariable", order = 10)]
public class IntVariableSO : ScriptableObject
{
    [SerializeField]
    private int value;

    [SerializeField]
    private int defaultValue;

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

    public void AddAmount(int amount)
    {
        value += amount;
        OnValueChanged.Invoke(value);
    }

    public void ResetValue()
    {
        value = defaultValue;
    }

    public int Value
    {
        get {return value;}
    }

    public int DefaultValue
    {
        get {return defaultValue;}
    }
}
