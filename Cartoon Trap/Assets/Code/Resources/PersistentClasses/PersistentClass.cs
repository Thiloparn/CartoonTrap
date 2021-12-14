using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentClass
{
    protected string saveFile;
    protected Dictionary<string, string> fileDatta;

    public abstract void SafeValue(string varibaleName);

    public abstract void LoadValue(string variableName);
}
