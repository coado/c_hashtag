﻿using System;
using System.Threading;


class Program {
    
    static public void Main() {
        Program program = new Program();
        program.zad1();
    }


    public void zad1() {
        
        List<Producent> producents = new List<Producent>();
        List<Konsument> consuments = new List<Konsument>();
        
        List<int> data = new List<int>();
        Random random = new Random(Environment.TickCount);

        int n = 5;
        int m = 4;

        for (int i = 0; i < n; i++) {
            Producent p = new Producent(i, data, random.Next(10000));
            p.Thread = new Thread(new ThreadStart(p.Start));
            producents.Add(p);
        }

        for (int i = 0; i < m; i++) {
            Konsument k = new Konsument(n + i, data, random.Next(10000));
            k.Thread = new Thread(new ThreadStart(k.Start));
            consuments.Add(k);
        }

        foreach(Producent producent in producents) {
            producent.Thread.Start();
        }

        foreach(Konsument consument in consuments) {
            consument.Thread.Start();
        }
        
        string command = "";
        while (command != "p") {
            command = Console.ReadLine();
        }

        foreach(Producent producent in producents) {
            producent.end = true;
        }

        foreach(Konsument consument in consuments) {
            consument.end = true;
        }
        
    }
}