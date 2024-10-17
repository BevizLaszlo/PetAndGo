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
        new Visszavalthato("Glass (2 liter)", 2000, 180),
        new Visszavalthato("Glass (1,5 liter)", 1500, 170),
        new Visszavalthato("Glass (1 liter)", 1000, 150),
        new Visszavalthato("PET (0,5 liter)", 500, 50)
    };

    public List<Visszavalthato> visszavalthatok;

    // Start is called before the first frame update
    void Start()
    {
        visszavalthatok = Visszavalthato.Visszavalthatok;
        Kiiratas();
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
                Visszavalthato.Visszavalthatok = visszavalthatok;
            }

        Kiiratas();
        HozzaadasPanel.gameObject.SetActive(false);
    }

    private void Kiiratas()
    {
        Kimenet.text = "";
        var temp_v = visszavalthatok.OrderBy(v => v.ErtekPerTerfogat).ToList();
        foreach (var v in temp_v.GroupBy(v => v.Nev))
        {
            Kimenet.text += $"{v.Key} - {v.Count()} - {v.Count() * v.First().ErtekAr} Ft\n";
        }
    }

    public void ShowHozzaadas()
    {
        HozzaadasPanel.gameObject.SetActive(true);
    }

}
