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

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        Kimenet.text = string.Empty;
        Ertek = Visszavalthatok.Sum(x => x.ErtekAr);
        Kimenet.color = Color.white;

        double max = 0;
        if (!double.TryParse(MaxSize.text.Trim(), out max))
        {
            Kimenet.color = Color.red;
            Kimenet.text = $"You have given an invalid bag size: {MaxSize.text} ({MaxSize.text.GetType()}).";
            return;
        }

        double endgoal = 0;
        if (!double.TryParse(PriceGoal.text.Trim(), out endgoal))
        {
            Kimenet.color = Color.red;
            Kimenet.text = $"You have given an invalid target amount: {PriceGoal.text} ({PriceGoal.text.GetType()}).";
            return;
        }

        if (endgoal > Ertek)
        {
            Kimenet.color = Color.yellow;
            Kimenet.text = $"Unfortunately, your current bottles do not reach the target amount.\nThis is how much money is missing: {endgoal - Ertek} Ft";
            return;
        }
        if (Visszavalthatok.Count == 0) return;

        double ossz = 0;
        int round = 1;
        temp_list = Visszavalthatok.OrderByDescending(x => x.ErtekPerTerfogat).ToList();
        Kimenet.text += $"Round {round}\n";

        int[] terfogatok = { 500, 1000, 1500, 2000 };
        int cm = temp_list.Count;
        for (int i = 0; i < cm; i++)
        {

            foreach (int terfogat in terfogatok)
            {
                if (ossz + terfogat <= max && temp_list.Count(x => x.Terfogat.Equals(terfogat)) != 0)
                {
                    Kimenet.text += $"\t{temp_list.Where(x => x.Terfogat.Equals(terfogat)).First().Nev}\n";
                    temp_list.Remove(temp_list.Where(x => x.Terfogat.Equals(terfogat)).First());
                    ossz += terfogat;
                    break;
                }
            }


            if (ossz + 500 > max && temp_list.Count > 0)
            {
                Kimenet.text += $"Round {++round}\n";
                ossz = 0;
            }
        }

        Kimenet.text += "\n\nFull Price: " + Ertek + " Ft";
    }
}
