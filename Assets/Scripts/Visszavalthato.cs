
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visszavalthato
{
    public string Nev { get; set; }
    public double Terfogat { get; set; }

    public Visszavalthato() { }

    public Visszavalthato(string nev, double terfogat)
    {
        this.Nev = nev;
        this.Terfogat = terfogat;
    }


    public Visszavalthato(Visszavalthato old)
    {
        this.Nev = old.Nev;
        this.Terfogat = old.Terfogat;
    }
}
