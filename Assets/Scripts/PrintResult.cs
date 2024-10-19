using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Visszavalthato;
using System.Linq;
using System.Globalization;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PrintResult : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Kimenet;
    [SerializeField] public TMP_InputField MaxSize;
    [SerializeField] public TMP_InputField PriceGoal;
    public static List<Visszavalthato> temp_list = new();
    public static string Size;
    public static int Ertek;


    public void Print()
    {
        Kimenet.text = string.Empty;
        Ertek = Visszavalthatok.Sum(x => x.ErtekAr);
        Kimenet.color = new Color(210, 210, 210);
        bool hasError = false;

        double max = 0;
        if (!double.TryParse(MaxSize.text.Trim(), out max) || max <= 0)
        {
            Kimenet.color = Color.red;
            Kimenet.text += $"You have given an invalid bag size: {"{" + MaxSize.text + "}"}.\n\n";
            hasError = true;
        }

        double endgoal = 0;
        if (!double.TryParse(PriceGoal.text.Trim(), out endgoal) || endgoal <= 0)
        {
            Kimenet.color = Color.red;
            Kimenet.text += $"You have given an invalid target amount: {"{" + PriceGoal.text + "}"}.";
            hasError= true;
        }
        if (hasError) return;



        if (endgoal > Ertek)
        {
            Kimenet.color = Color.yellow;
            Kimenet.text = $"Unfortunately, your current bottles do not reach the target amount.\nThis is how much money is missing: {endgoal - Ertek} Ft";
            return;
        }
        if (Visszavalthatok.Count == 0) return;

        double teljesOsszeg = 0;
        double forduloTerfogat = 0;
        int round = 1;
        temp_list = Visszavalthatok.OrderByDescending(x => x.ErtekPerTerfogat).ToList();
        Kimenet.text += $"Round {round}\n";

        int[] terfogatok = { 500, 1000, 1500, 2000 };
        int cm = temp_list.Count;
        for (int i = 0; i < cm; i++)
        {
            foreach (int terfogat in terfogatok)
            {
                if (forduloTerfogat + terfogat <= max && temp_list.Count(x => x.Terfogat.Equals(terfogat)) != 0)
                {
                    Visszavalthato temp = temp_list.Where(x => x.Terfogat.Equals(terfogat)).First();
                    Kimenet.text += $"\t{temp.Nev}\n";
                    temp_list.Remove(temp);
                    forduloTerfogat += terfogat;
                    teljesOsszeg += temp.ErtekAr;
                }
            }

            if (teljesOsszeg >= endgoal) break;

            if (forduloTerfogat + 500 > max && temp_list.Count > 0)
            {
                Kimenet.text += $"Round {++round}\n";
                forduloTerfogat = 0;
            }
        }

        if (teljesOsszeg >= endgoal)
            Kimenet.text += "\n\nFull Price: " + teljesOsszeg + " Ft";
        else
        {
            Kimenet.color = Color.yellow;
            Kimenet.text = $"Unfortunately, your current bag size doesn't allow you to reach the target amount.\nThis is how much money is missing: {endgoal - teljesOsszeg} Ft";
        }
    }
}
