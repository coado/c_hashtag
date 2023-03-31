
public class Producent {
    public int serialNumber;
    public List<int> data;
    public int timeout;
    public Thread Thread = null;    
    public bool end = false;

    public Producent(int SerialNumber, List<int> Data, int Timeout) {
        this.serialNumber = SerialNumber;
        this.data = Data;
        this.timeout = Timeout;
    }

    public void Start() {
        while(!end) {
           Thread.Sleep(timeout);
           WriteResource(); 
        }
    }

    public void WriteResource() {
        // mutex.WaitOne();
        lock (data) {
            
            this.data.Add(serialNumber);
        }
    }
}