using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorOptionData : TMP_Dropdown.OptionData
{
    public Color Color { get; set; }

    public ColorOptionData(string text, Color color) : base(text)
    {
        this.Color = color;
    }
}
