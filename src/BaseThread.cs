using System.Threading;

abstract class BaseThread
{
    private Thread _thread;

    protected BaseThread() { _thread = new Thread(new ThreadStart(this.RunThread)); }

    // Métodos de los hilos
    public void Start() { _thread.Start(); }
    public void Join() { _thread.Join(); }
    public bool IsAlive { get { return _thread.IsAlive; } }

    // Sobreescribe la clase base
    public abstract void RunThread();
}
