


public class SearchFiles {
    public string dir;
    public string nameToFind;
    public List<string> files;
    public Thread Thread = null;

    public SearchFiles(string Dir, string nameToFind, List<string> files) {
        this.dir = Dir;
        this.nameToFind = nameToFind;
        this.files = files;
    }

    public void Start() {
        Search();
    }

    public void Search() {
        lock (files) {
            string[] searched_files = Directory.GetFiles(dir, "*"); 

            foreach (string file in searched_files) {
                string fileName = Path.GetFileName(file);
                
                if (fileName.Contains(nameToFind))
                    this.files.Add(fileName);
            }
        }
    }
}