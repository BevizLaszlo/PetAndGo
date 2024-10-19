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

        List<List<Visszavalthato>> rounds = new();
        rounds.Add(new List<Visszavalthato>());

        temp_list = Visszavalthatok.OrderByDescending(x => x.ErtekPerTerfogat).ToList();

        // Calculation
        int[] terfogatok = { 2000, 1500, 1000, 500 };
        int cm = temp_list.Count;
        for (int i = 0; i < cm; i++)
        {
            foreach (int terfogat in terfogatok)
            {
                if (rounds.Last().Sum(x => x.Terfogat) + terfogat <= max && temp_list.Count(x => x.Terfogat.Equals(terfogat)) != 0)
                {
                    Visszavalthato temp = temp_list.Where(x => x.Terfogat.Equals(terfogat)).First();
                    temp_list.Remove(temp);
                    rounds.Last().Add(temp);
                    break;
                }
            }

            if (rounds.Sum(x => x.Sum(y => y.ErtekAr)) >= endgoal) break;

            if (temp_list.Count > 0 && rounds.Last().Sum(x => x.Terfogat) + temp_list.OrderBy(x => x.Terfogat).First().Terfogat > max)
                rounds.Add(new List<Visszavalthato>());
        }



        // Render
        if (rounds.Sum(x => x.Sum(y => y.ErtekAr)) >= endgoal)
        {
            for (int i = 0; i < rounds.Count; i++)
            {
                Kimenet.text += $"<color=#216AB0>Round {i + 1}</color>\n";

                foreach(Visszavalthato item in rounds[i])
                    Kimenet.text += $"\t{item.Nev}\n";

                Kimenet.text += $"\t-----------\n";
                Kimenet.text += $"\t{rounds[i].Sum(x => x.ErtekAr)} Ft\n\n";
            }
            Kimenet.text += $"<color=#216AB0>Total amount: {rounds.Sum(x => x.Sum(y => y.ErtekAr))} Ft</color>";
        }
        else
        {
            Kimenet.color = Color.yellow;
            Kimenet.text = $"Unfortunately, your current bag size doesn't allow you to reach the target amount.\nThis is how much money is missing: {endgoal - rounds.Sum(x => x.Sum(y => y.ErtekAr))} Ft";
        }
    }
}
