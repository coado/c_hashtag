


public class FileWatcher {
        public Thread Thread = null;
        public string watchPath;
        public bool end = false;

        public FileWatcher(string WathParh) {
            this.watchPath = WathParh;
        }
 
        public void Start() { 
            FileSystemWatcher watcher = new FileSystemWatcher(watchPath);

            watcher.EnableRaisingEvents = true;
            watcher.Created += (sender, e) => Console.WriteLine($"{e.Name} created");
            watcher.Changed += (sender, e) => Console.WriteLine($"{e.Name} changed");
            watcher.Renamed += (sender, e) => Console.WriteLine($"changed from {e.OldName} to {e.Name}");
            watcher.Deleted += (sender, e) => Console.WriteLine($"{e.Name} deleted");

            while (!end) {
                Console.WriteLine("FileWatcher started");
                string command = Console.ReadLine();

                if (command != "q") continue;

                watcher.EnableRaisingEvents = false;
                break;
            }        
        }
}