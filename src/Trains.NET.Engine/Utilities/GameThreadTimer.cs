using System;
using System.Diagnostics;
using System.Threading;

namespace Trains.NET.Engine
{
    [Transient]
    public class GameThreadTimer : ITimer
    {
        public double Interval { get; set; }
        public long TimeSinceLastTick { get; private set; }

        public event EventHandler? Elapsed;

        private bool _elapsedEventEnabled;
        private long _lastTick;
        private bool _threadLoopEnabled = true;
        private long _nextInvoke;

        private readonly Thread _gameThread;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public GameThreadTimer()
        {
            _gameThread = new Thread(ThreadLoop);
            _gameThread.Start();
        }

        private void ThreadLoop()
        {
            while(_threadLoopEnabled)
            {
                while (_threadLoopEnabled && _stopwatch.ElapsedMilliseconds < _nextInvoke) ;
                if (_threadLoopEnabled && _elapsedEventEnabled)
                {
                    long time = _stopwatch.ElapsedMilliseconds;
                    this.TimeSinceLastTick = time - _lastTick;
                    _lastTick = time;
                    Elapsed?.Invoke(null, null);

                    _nextInvoke = time + (int)this.Interval;
                }
            }
        }

        public void Dispose()
        {
            _threadLoopEnabled = false;
        }

        public void Start() => _elapsedEventEnabled = true;

        public void Stop() => _elapsedEventEnabled = false;
    }
}
