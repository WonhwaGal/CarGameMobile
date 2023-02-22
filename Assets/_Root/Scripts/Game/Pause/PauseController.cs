using System.Collections.Generic;
using UnityEngine;

namespace Tool.Pause
{
    internal class PauseController : MonoBehaviour, IPauseHandler
    {
        public static PauseController Instance;

        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        private void Awake()
        {
            Instance ??= this;
        }
        public bool IsPaused { get; private set; }

        public void Register(IPauseHandler handler)
        {
            _pauseHandlers.Add(handler);
        }

        public void UnRegister(IPauseHandler handler)
        {
            _pauseHandlers.Remove(handler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (IPauseHandler handler in _pauseHandlers)
            {
                handler.SetPaused(isPaused);
            }
        }
    }
}

