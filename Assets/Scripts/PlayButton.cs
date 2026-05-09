using Bodybuilder.Util.Time;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bodybuilder
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Timer _waitTimer;

        private void Awake()
        {
            _waitTimer.OnInterval.AddListener(Play);
        }

        private void Update()
        {
            _waitTimer.Update();
        }

        public void WaitAndPlay()
        {
            if(_waitTimer.Running) { return; }
            _waitTimer.Start();
        }

        public void Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}
