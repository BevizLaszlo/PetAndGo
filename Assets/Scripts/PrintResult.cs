using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Visszavalthato;

public class PrintResult : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Kimenet;
    [SerializeField] public TextMeshProUGUI MaxSize;
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
            Ertek += v.ErtekAr;
        }
        Size = MaxSize.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {
        double max = 5000;
        double ossz = 0;
        int round = 1;
        Kimenet.text = $"{round}. round\n";
        foreach (var v in temp_list) {
            ossz += v.Terfogat;
            if(ossz > max)
            {
                round++;
                Kimenet.text += $"{round}. round\n";
                ossz = v.Terfogat ;
            }
            Kimenet.text += "\t" + v.Nev + "\n";
        }
        Kimenet.text += "\n\nAz összérték: " + Ertek + " Ft";
        //Kimenet.text = $"{round} round\n";
        //while (List1L.Count != 0)
        //{
        //    Kimenet.text = $"{round} round\n";
        //    while (List1L.Count != 0 && List1L[0].Terfogat + ossz < max)
        //    {
        //        Kimenet.text += $"\t{List1L[0].Nev} {List1L[0].ErtekAr}\n";
        //        ossz += List1L[0].Terfogat;
        //        List1L.RemoveAt(0);
        //    }
        //    while (List0_5L.Count != 0 && List0_5L[0].Terfogat + ossz < max)
        //    {
        //        Kimenet.text += $"\t{List0_5L[0].Nev} {List0_5L[0].ErtekAr}\n";
        //        ossz += List0_5L[0].Terfogat;
        //        List0_5L.RemoveAt(0);
        //    }
        //    ossz = 0;
        //    round++;
        //}
        //if (List1L.Count == 0) Kimenet.text += "Hello";
        //if(Visszavalthatok.Count != 0)
        //{
        //    for (int i = 0; i < Visszavalthatok.Count; i++)
        //    {
        //        Kimenet.text += Visszavalthatok[i].Nev + "\n";
        //    }
        //}
    }
}
