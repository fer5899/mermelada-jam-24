using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IntReference
{
    public bool useConstant = true;
    public int constantValue;

    public IntVariableSO variable;

    public int multiplier = 1;

    public int value
    {
        get { return useConstant ? constantValue : variable.value * multiplier; }
    }
}
