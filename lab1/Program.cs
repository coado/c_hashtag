using System;
using System.IO;
using System.Linq;



class Program {
    static void Main(string[] args) {
        
    //    Program.zad2();
        // Program.zad3();
        // Program.zad4();
        // Program.zad5();
    }

    public static void zad2() {
        string nazwaPliku = "plik.txt";
        StreamWriter sw;
        
        if (File.Exists(nazwaPliku))
            sw = new StreamWriter(nazwaPliku, append: true);
        else
            sw = new StreamWriter(nazwaPliku);

        string ostatni_napis = "";

        string napis;

       while (true) {
            Console.Write("Wpisz napis: ");
            napis = Console.ReadLine();

            if (napis == "koniec!") break;

            sw.WriteLine(napis);
            
            int comparing = String.Compare(napis, ostatni_napis);

            if (napis != null && comparing > 0) {
                ostatni_napis = napis;  
            }
       }

        sw.WriteLine(ostatni_napis);
        sw.Close();
    }

    public static void zad3() {
        string line = Console.ReadLine();
        if (line == null) return;

        string[] req_params = line.Split(" ");

        if (req_params.Length != 2) return;

        string file_name = req_params[0];
        string chars = req_params[1];

        int line_counter = 0;

        StreamReader sr = new StreamReader(file_name);

        while (!sr.EndOfStream) {
            string read_line = sr.ReadLine();
            line_counter++;
            int index = read_line.IndexOf(chars);

            if (index != -1) {
                Console.WriteLine("linijka: " + line_counter + ", pozycja: " + index);
            }
        }

        sr.Close();
    }

    public static void zad4() {
        string line = Console.ReadLine();
        if (line == null) return;

        string[] req_params = line.Split(" ");

        if (req_params.Length != 5) return;

        string file_name = req_params[0];
        int n = (int)Convert.ToInt64(req_params[1]);
        string range = req_params[2];
        int seed = (int)Convert.ToInt64(req_params[3]);
        int r = (int)Convert.ToInt64(req_params[4]);

        string[] range_params = range.Split('-');
        int l = (int)Convert.ToInt64(range_params[0]);
        int h = (int)Convert.ToInt64(range_params[1]);


        StreamWriter sw;

        if (File.Exists(file_name))
            sw = new StreamWriter(file_name, append: true);
        else
            sw = new StreamWriter(file_name);


        Random random = new Random(seed);

        for (int i = 0; i < n; i++) {
            if (r == 0) {
                int value = random.Next(l,h);
                sw.WriteLine(value);
            } else {
               double value = random.NextDouble();
               sw.WriteLine(value);
            }

        }

        sw.Close();
    }

    public static void zad5() {
        string file_name = Console.ReadLine();
        if (file_name == null) return;

        var lines_read = File.ReadLines(file_name);

        Console.WriteLine(lines_read.Count());
        Console.WriteLine(File.ReadLines(file_name).Sum(s => s.Length));

        double highest = -1;
        double lowest = 10000000;
        double sum = 0;
        double counter = 0;

        foreach (string line in lines_read) {
            double value = (double)Convert.ToDouble(line);

            if (value > highest) highest = value;
            if (value < lowest) lowest = value;
            sum += value;
            counter++;
        }

        Console.WriteLine(highest);
        Console.WriteLine(lowest);
        Console.WriteLine(sum / counter);

    }
}