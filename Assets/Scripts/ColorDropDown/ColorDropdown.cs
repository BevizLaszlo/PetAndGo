using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// https://www.youtube.com/watch?v=N0afV6h2L6o
public class ColorDropdown : TMP_Dropdown
{
    private const int BackgroundItemIndex = 0;
    private int _dataIndex = 0;

    protected override GameObject CreateDropdownList(GameObject template)
    {
        _dataIndex = 0;
        return base.CreateDropdownList(template);
    }

    protected override DropdownItem CreateItem(DropdownItem itemTemplate)
    {
        var item = base.CreateItem(itemTemplate);

        var backgroundTemplate = item.transform.GetChild(BackgroundItemIndex);
        var image = backgroundTemplate.GetComponent<Image>();

        var data = this.options[_dataIndex];
        if (data is ColorOptionData colorOptionData)
        {
            image.color = colorOptionData.Color;
        }
        _dataIndex++;
        return item;
    }
}
