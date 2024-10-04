using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
public class InitDropdown : MonoBehaviour
{
    private TMP_Dropdown Dropdown;
    // Start is called before the first frame update
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        Dropdown.AddOptions(AppLogic.Instance.Tipusok.Select(t => t.Nev).ToList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
