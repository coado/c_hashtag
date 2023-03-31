using System;
using System.Collections.Generic;
using System.IO;

// zad1
public abstract class PosiadaczRachunku {
    public abstract override string ToString();
}


// zad2
class OsobaFizyczna:PosiadaczRachunku {
    private string imie;
    private string nazwisko;
    private string drugieImie;
    private int[] PESEL;
    private string numerPaszportu;

    public string NumerPaszportu { get => numerPaszportu; set => numerPaszportu = value; }
    public string Imie { get => imie; set => imie = value; }
    public string Nazwisko { get => nazwisko; set => nazwisko = value; }
    public string DrugieImie { get => drugieImie; set => drugieImie = value; }
    public int[] PESEL1 { get => PESEL; set {
        if (value == null || value.Length != 11) throw new Exception("bledny pesel");
        
        PESEL = value;
    } }

    public OsobaFizyczna(string imie, string nazwisko, string drugieImie, int[] pesel, string numerPaszportu) {
        if (pesel == null && numerPaszportu == null ) throw new Exception("brak peselu i numeru paszportu");
        if (pesel == null || pesel.Length != 11) throw new Exception("bledny pesel");

        this.imie = imie;
        this.nazwisko = nazwisko;
        this.drugieImie = drugieImie;
        this.PESEL = pesel;
        this.numerPaszportu = numerPaszportu;
    }   

    public override string ToString() => "Osoba fizyczna: " + imie + " " + nazwisko;
}


// zad 4
class OsobaPrawna:PosiadaczRachunku {
    private string nazwa;
    private string siedziba;

    public string Nazwa { get => nazwa; }
    public string Siedziba { get => siedziba; }

    public OsobaPrawna(string nazwa, string siedziba) {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    public override string ToString() => "Osoba Prawna: " + nazwa + " " + siedziba;
}

public class RachunekBankowy {
    private string number;
    private decimal stanRachunku;
    private bool czyDozwolonyDebet;

    private List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();

    private List<Transakcja> _Transakcje = new List<Transakcja>();

    public string Number { get => number; set => number = value; }
    public decimal StanRachunku { get => stanRachunku; set => stanRachunku = value; }
    public bool CzyDozwolonyDebet { get => czyDozwolonyDebet; set => czyDozwolonyDebet = value; }
    public List<PosiadaczRachunku> PosiadaczeRachunku { get => _PosiadaczeRachunku; set => _PosiadaczeRachunku = value; }

    public RachunekBankowy(string number, decimal stanRachunku, bool czyDozwolonyDebet,  List<PosiadaczRachunku> _PosiadaczeRachunku) {
        if (_PosiadaczeRachunku.Count == 0) throw new Exception("Brak posiadaczy rachunku");

        this.number = number;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        this._PosiadaczeRachunku = _PosiadaczeRachunku;
    }

    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku nowy_posiadacz) {
            if (rachunek.PosiadaczeRachunku.Contains(nowy_posiadacz)) throw new Exception("posiadacz jest juz dodany do rachunku");
            
            rachunek._PosiadaczeRachunku.Add(nowy_posiadacz);
            return rachunek;
    }

    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku posiadacz) {
        if (rachunek.PosiadaczeRachunku.Count == 1) throw new Exception("zbyt mala liczba posiadaczy rachunku");
        if (!rachunek.PosiadaczeRachunku.Contains(posiadacz)) throw new Exception("podany posiadacz nie znajduje sie na liscie");
    
        rachunek._PosiadaczeRachunku.Remove(posiadacz);
        return rachunek;
    }   

    public override string ToString() {
        string opis = number + " " + stanRachunku;

        foreach (PosiadaczRachunku posiadacz in _PosiadaczeRachunku) {
            opis = opis + " " + posiadacz.ToString();
        }

        foreach(Transakcja tx in _Transakcje) {
            opis = opis + " " + tx.ToString();
        }

        return opis;
    }


    public static void DokonajTransaskcji(
        RachunekBankowy rachunekZrodlowy, 
        RachunekBankowy rachunekDocelowy,
        decimal kwota,
        string opis    
    ) {
        if (kwota < 0) throw new Exception("ujemna kwota transakcji");
        if (rachunekZrodlowy == null && rachunekDocelowy == null) throw new Exception("brak rachunkow");
        if (rachunekZrodlowy != null && !rachunekZrodlowy.czyDozwolonyDebet && kwota > rachunekZrodlowy.stanRachunku) throw new Exception("kwota transakcji jest zbyt duza");
    
        if (rachunekZrodlowy == null) {
            rachunekDocelowy.stanRachunku += kwota;
            Transakcja tx = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekDocelowy._Transakcje.Add(tx);
        } else if (rachunekDocelowy == null) {
            rachunekZrodlowy.stanRachunku -= kwota;
            Transakcja tx = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekZrodlowy._Transakcje.Add(tx);
        } else {
            rachunekZrodlowy.stanRachunku -= kwota;
            rachunekDocelowy.stanRachunku += kwota;
            Transakcja tx = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekZrodlowy._Transakcje.Add(tx);
            rachunekDocelowy._Transakcje.Add(tx);
        }
    
    }
}

class Transakcja {
    private RachunekBankowy? rachunekZrodlowy;
    private RachunekBankowy? rachunekDocelowy;
    private decimal kwota;
    private string opis;

    public decimal Kwota { get => kwota; set => kwota = value; }
    public string Opis { get => opis; set => opis = value; }
    private RachunekBankowy? RachunekZrodlowy { get => rachunekZrodlowy; set => rachunekZrodlowy = value; }
    private RachunekBankowy? RachunekDocelowy { get => rachunekDocelowy; set => rachunekDocelowy = value; }

    public Transakcja(RachunekBankowy? rachunekZrodlowy, RachunekBankowy? rachunekDocelowy, decimal kwota, string opis) {
        if (rachunekZrodlowy == null || rachunekDocelowy == null) throw new Exception("Brak rachunku");

        this.rachunekZrodlowy = rachunekZrodlowy;
        this.rachunekDocelowy = rachunekDocelowy;
        this.kwota = kwota;
        this.opis = opis;
    }

    public override string ToString() => (rachunekZrodlowy != null ? rachunekZrodlowy.Number : "brak") + " -> " + (rachunekDocelowy != null ? rachunekDocelowy.Number : "brak") + ", kwota: " + kwota + ", " + opis;
}


class Program {
    static public void Main(string[] args) {
        int[] pesel = new int[11] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
        OsobaFizyczna osoba1 = new OsobaFizyczna("jan", "nowak", "kacper", pesel, "123");
        OsobaFizyczna osoba2 = new OsobaFizyczna("piotr", "kowalski", "jakub", pesel, "1234");

        RachunekBankowy rachunek1 = new RachunekBankowy("123123123", 1000, true, new List<PosiadaczRachunku> { osoba1 });
        RachunekBankowy rachunek2 = new RachunekBankowy("5555555", 2000, false, new List<PosiadaczRachunku> { osoba2 });

        RachunekBankowy.DokonajTransaskcji(rachunek1, rachunek2, 100, "opis");

        rachunek1 += osoba2;
        rachunek1 -= osoba1;
        rachunek1 -= osoba1;

        System.Console.WriteLine(rachunek1);
        System.Console.WriteLine(rachunek2);

    }
}