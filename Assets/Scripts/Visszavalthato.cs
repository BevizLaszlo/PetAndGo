
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visszavalthato
{
    public static List<Visszavalthato> Visszavalthatok = new List<Visszavalthato>();

    public string Nev { get; set; }
    public double Terfogat { get; set; }
    
    public int ErtekAr { get; set; }
    public double ErtekPerTerfogat { get; set; }

    public Visszavalthato() { }

    public Visszavalthato(string nev, double terfogat, int ertekAr)
    {
        this.Nev = nev;
        this.Terfogat = terfogat;
        this.ErtekAr = ertekAr;

        this.ErtekPerTerfogat = this.ErtekAr / this.Terfogat;
    }


    public Visszavalthato(Visszavalthato old)
    {
        this.Nev = old.Nev;
        this.Terfogat = old.Terfogat;
        this.ErtekAr = old.ErtekAr;

        this.ErtekPerTerfogat = this.ErtekAr / this.Terfogat;
    }
}
