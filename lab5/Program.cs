using System;
using System.Threading;


class Program {
    
    static public void Main() {
        Program program = new Program();
        program.zad1();
        // program.zad2();
        // program.zad3();
        // program.zad4();
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
        while (command != "q") {
            command = Console.ReadLine();
        }

        foreach(Producent producent in producents) {
            producent.end = true;
        }

        foreach(Konsument consument in consuments) {
            consument.end = true;
        }
        
    }

    public void zad2() {
        FileWatcher fileWatcher = new FileWatcher("./");
        fileWatcher.Thread = new Thread(new ThreadStart(fileWatcher.Start));
        fileWatcher.Thread.Start();
    }

    public void zad3() {
        List<string> fileNames = new List<string>();

        SearchFiles searchFiles = new SearchFiles("./", "", fileNames);
        searchFiles.Thread = new Thread(new ThreadStart(searchFiles.Start));
        searchFiles.Thread.Start();  

        while (true) {
            foreach (string fileName in fileNames.ToList()) {
                Console.WriteLine(fileName);
            }   

            Thread.Sleep(2000);
        }

    }

    public void zad4() {

        List<int> wakeupIds = new List<int>();
        Random random = new Random(Environment.TickCount);
        List<SleepyThread> sleepyThreads = new List<SleepyThread>();
        
        int n = 10;

        for (int i = 0; i < n; i++) {
            SleepyThread sleepyThread = new SleepyThread(i, wakeupIds, random.Next(10000));
            sleepyThread.Thread = new Thread(new ThreadStart(sleepyThread.Start));
            sleepyThreads.Add(sleepyThread);
        }

        foreach(SleepyThread thread in sleepyThreads) {
            thread.Thread.Start();
        }

        while(wakeupIds.Count != n) {}
    
        Console.WriteLine("All threads are up");

        foreach (SleepyThread thread in sleepyThreads) {
            thread.end = true;
        }

        Console.WriteLine("Program closed");
    }

}