using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AppLogic : MonoBehaviour
{
    [SerializeField] private GameObject HozzaadasPanel;
    [SerializeField] private TMP_Dropdown Dropdown;
    [SerializeField] private TMP_InputField Darab;
    [SerializeField] private TextMeshProUGUI KimenetDebug;
    [SerializeField] private GameObject KimenetPanel;
    [SerializeField] private GameObject KimenetPrefab;
    [SerializeField] private TMP_Text TotalAmount;
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
        Destroy(KimenetPanel.transform.GetChild(0).gameObject);
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
        if (KimenetDebug.IsActive())
        {
            KimenetDebug.text = "";
            var temp_v = visszavalthatok.OrderBy(v => v.ErtekPerTerfogat).ToList();
            foreach (var v in temp_v.GroupBy(v => v.Nev))
            {
                KimenetDebug.text += $"{v.Key} - {v.Count()} - {v.Count() * v.First().ErtekAr} Ft\n";
            }
        }
        foreach(Transform child in KimenetPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < visszavalthatok.Count; i++)
        {
            var p = Instantiate(KimenetPrefab);
            p.GetComponentInChildren<DeleteElem>().id = i;
            p.transform.SetParent(KimenetPanel.transform, false);
            p.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{visszavalthatok[i].Nev} - {visszavalthatok[i].ErtekAr}Ft";
            p.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                int id = p.GetComponentInChildren<DeleteElem>().id;
                AppLogic.Instance.visszavalthatok.RemoveAt(id);
                AppLogic.Instance.Kiiratas();
            });
        }
        UpdateTotalAmountText();
    }

    public void ShowHozzaadas()
    {
        HozzaadasPanel.gameObject.SetActive(true);
    }

    public void UpdateTotalAmountText() => TotalAmount.text = $"Total: {visszavalthatok.Sum(x => x.ErtekAr)} Ft";

}
