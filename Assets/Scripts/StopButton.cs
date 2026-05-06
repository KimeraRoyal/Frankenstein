using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class StopButton : MonoBehaviour
{
    [SerializeField] private Timer _waitTimer;

    private void Awake()
    {
        _waitTimer.OnInterval.AddListener(Stop);
    }

    private void Update()
    {
        _waitTimer.Update();
    }

    public void WaitAndStop()
    {
        if(_waitTimer.Running) { return; }
        _waitTimer.Start();
    }

    public void Stop()
    {
        Application.Quit();
    }
}
