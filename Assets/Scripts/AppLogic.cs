using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AppLogic : MonoBehaviour
{
    [SerializeField] private GameObject HozzaadasPanel;
    [SerializeField] private TMP_Dropdown Dropdown;
    [SerializeField] private TMP_InputField Darab;
    [SerializeField] private TextMeshProUGUI Kimenet;
    public static AppLogic Instance;
    public int VisszavaltasiAr = 50; // HUF

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public List<Visszavalthato> Tipusok = new()
    {
        new Visszavalthato("Üveg (1 liter)", 1000),
        new Visszavalthato("PET (0,5 liter)", 500)
    };

    public List<Visszavalthato> visszavalthatok;

    // Start is called before the first frame update
    void Start()
    {
        visszavalthatok = new();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add()
    {
        int db = int.Parse(Darab.text);
        if (db > 0)
            for (int i = 0; i < db; i++)
            {
                visszavalthatok.Add(
                    new Visszavalthato(Tipusok.First(t => t.Nev == Dropdown.options[Dropdown.value].text)));
            }

        Kimenet.text = "";
        foreach (var v in visszavalthatok)
        {
            Kimenet.text += $"{v.Nev}\n";
        }
        HozzaadasPanel.gameObject.SetActive(false);
    }

    public void ShowHozzaadas()
    {
        HozzaadasPanel.gameObject.SetActive(true);
    }

}
