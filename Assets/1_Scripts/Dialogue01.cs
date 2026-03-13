using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue01
{
    public string name;

    [TextArea(3,20)]
    public string[] sentences;
}
