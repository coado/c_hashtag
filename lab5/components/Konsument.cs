


public class Konsument {
    public int serialNumber;
    public List<int> data;
    public int timeout;
    public bool end = false;
    public Thread Thread = null;
    public List<int> consumed = new List<int>();

    public Konsument(int SerialNumber, List<int> Data, int Timeout) {
        this.serialNumber = SerialNumber;
        this.data = Data;
        this.timeout = Timeout;
    }

    public void Start() {
        while(!end) {
           Thread.Sleep(timeout);
           ReadResource(); 
        }

        Console.WriteLine($"Consumer: {serialNumber} consumed: {ToString()}");
    }

    public void ReadResource() {
        lock (data) {
            if (data.Count == 0) return;
            consumed.Add(data[data.Count - 1]);
            data.RemoveAt(data.Count - 1);
        }
    }

    public override string ToString() {
        string output = "";

        foreach(int v in consumed) 
            output += " " + v;
        
        return output;
    }
}