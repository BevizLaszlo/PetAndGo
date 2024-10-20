using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class InitDropdown : MonoBehaviour
{
    private ColorDropdown Dropdown;
    // Start is called before the first frame update
    void Start()
    {
        Dropdown = GetComponent<ColorDropdown>();
        List<TMP_Dropdown.OptionData> options = new();
        foreach (var tipus in AppLogic.Instance.Tipusok)
        {
            options.Add(new ColorOptionData(tipus.Nev, tipus.Color));
        }
        Dropdown.AddOptions(options);
    }

    // Márknak: ez nem működött, és csak úgy tudtuk meghívni, ha elősször rákattintottunk az egyik itemre

    //void RecolorDropdownItems()
    //{
    //    // Wait for the dropdown to be fully rendered
    //    GameObject dropdownList = Dropdown.transform.Find("Dropdown List").gameObject;
    //    Transform dropdownContent = dropdownList.transform.Find("Viewport/Content");

    //    // Loop through each child of Content (each dropdown item)
    //    for (int i = 0; i < dropdownContent.childCount; i++)
    //    {
    //        // Get the child Item (dropdown item)
    //        Transform item = dropdownContent.GetChild(i);

    //        // Find the Image component that controls the background of the item
    //        Image itemBackground = item.GetComponentInChildren<Image>();

    //        if (itemBackground != null)
    //        {
    //            // Alternate background color (blue and white) for each dropdown item
    //            itemBackground.color = (i % 2 == 0) ? Color.blue : Color.white;
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
