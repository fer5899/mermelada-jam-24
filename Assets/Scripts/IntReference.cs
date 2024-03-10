using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Cecil.Cil;

[Serializable]
public class IntReference
{
    [SerializeField]
    private bool useConstant = true;
    [SerializeField]
    private int constantValue;
    [SerializeField]
    private IntVariableSO variable;
    [SerializeField]
    private int multiplier = 1;

    public int value
    {
        get { return useConstant ? constantValue : variable.Value * multiplier; }
    }

    public int defaultValue
    {
        get { return variable.DefaultValue; }
    }
}
