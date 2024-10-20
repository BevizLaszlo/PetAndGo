
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Visszavalthato
{
    public static List<Visszavalthato> Visszavalthatok = new List<Visszavalthato>();

    public string Nev { get; set; }
    public double Terfogat { get; set; }
    
    public Color Color { get; set; }
    public int ErtekAr { get; set; }
    public double ErtekPerTerfogat => this.ErtekAr / (this.Terfogat / 1000);
    public Visszavalthato() { }

    public Visszavalthato(string nev, double terfogat, int ertekAr)
    {
        this.Nev = nev;
        this.Terfogat = terfogat;
        this.ErtekAr = ertekAr;
        this.Color = Color.white;
    }

    public Visszavalthato(string nev, double terfogat, int ertekAr, Color szin)
    {
        this.Nev = nev;
        this.Terfogat = terfogat;
        this.ErtekAr = ertekAr;
        this.Color = szin;
    }
    
    public Visszavalthato(Visszavalthato old)
    {
        this.Nev = old.Nev;
        this.Terfogat = old.Terfogat;
        this.ErtekAr = old.ErtekAr;
        this.Color = old.Color;
    }
}
