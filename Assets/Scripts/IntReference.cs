using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Cecil.Cil;

[Serializable]
public class IntReference
{
    public bool useConstant = true;
    public int constantValue;

    public IntVariableSO variable;

    public int multiplier = 1;

    public int value
    {
        get { return useConstant ? constantValue : variable.Value * multiplier; }
    }

    public int defaultValue
    {
        get { return variable.DefaultValue; }
    }
}
