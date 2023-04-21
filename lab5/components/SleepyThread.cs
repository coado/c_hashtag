


public class SleepyThread {

    public int id;
    List<int> wakeupIds;
    public int timeout;
    public Thread Thread = null;    
    public bool end = false;
    public bool isWakedUp = false;


    public SleepyThread(int Id, List<int> WakeupIds, int Timeout) {
        this.id = Id;
        this.wakeupIds = WakeupIds;
        this.timeout = Timeout;
    }

    public void Start() {
        while(!end) {
            Thread.Sleep(timeout);

            // wake up after first timeout
            if (!isWakedUp) {
                lock (wakeupIds) {
                    Console.WriteLine($"Thread {id} is up");
                    wakeupIds.Add(id);
                }
                isWakedUp = true;
            }

            // doing some job...
        }
    }
}