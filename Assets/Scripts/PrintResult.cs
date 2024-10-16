using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Visszavalthato;
using System.Linq;
using System.Globalization;
using UnityEngine.UI;

public class PrintResult : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Kimenet;
    [SerializeField] public TMP_InputField MaxSize;
    [SerializeField] public TMP_InputField PriceGoal;
    public static List<Visszavalthato> List1L = new();
    public static List<Visszavalthato> List0_5L = new();
    public static List<Visszavalthato> temp_list = Visszavalthatok;
    public static string Size;
    public static int Ertek = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        foreach (var v in Visszavalthatok)
        {
            if (v.Terfogat.Equals(1000)) List1L.Add(v);
            else List0_5L.Add(v);
        }
        Size = MaxSize.text;
        Ertek = Visszavalthatok.Sum(v => v.ErtekAr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        Kimenet.text = string.Empty;

        double max = 0;
        if (!double.TryParse(MaxSize.text.Trim(), out max))
        {
            Kimenet.text = $"Helytelen táska méret adatot adott meg: {MaxSize.text} ({MaxSize.text.GetType()}).";
            return;
        }

        double endgoal = 0;
        if (!double.TryParse(PriceGoal.text.Trim(), out endgoal))
        {
            Kimenet.text = $"Helytelen pénzösszeg adatot adott meg: {PriceGoal.text} ({PriceGoal.text.GetType()}).";
            return;
        }

        if (endgoal > Ertek)
        {
            Kimenet.text = $"Sajnos jelenlegi palackjai nem érik el a célösszeget.\nEnnyi pénz hiányzik: {endgoal - Ertek} Ft";
            return;
        }

        double ossz = 0;
        int round = 1;
        Kimenet.text += $"{round}. round\n";
        temp_list = temp_list.OrderByDescending(x => x.ErtekPerTerfogat).ToList();

        foreach (var v in temp_list) {
            ossz += v.Terfogat;
            if(ossz > max)
            {
                round++;
                Kimenet.text += $"{round}. round\n";
                ossz = v.Terfogat ;
            }
            Kimenet.text += $"\t{v.Nev} ({v.ErtekPerTerfogat}Ft/l)\n";
        }
        Kimenet.text += "\n\nAz összérték: " + Ertek + " Ft";
    }
}
