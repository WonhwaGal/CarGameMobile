using System.Collections.Generic;


namespace Game
{
    internal class PauseMenuModel : IPauseHandler
    {
        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
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

