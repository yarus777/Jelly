using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.MyScripts.Lives
{
    class LivesManager
    {
        #region Instance management

        private static LivesManager _instance;


        private LivesManager()
        {
            _livesTimer = new GameObject("LivesTimer").AddComponent<Timer>();
            _livesTimer.Destroyed += OnTimerDestroy;
            Object.DontDestroyOnLoad(_livesTimer.gameObject);
            _livesTimer.Tick += OnTimer;
            var data = Load();
            Init(data);
        }

        private float _currentTimerInterval;

        private void Init(LivesData data)
        {
            var timeLeft = data.TimeLeftValue - (GetTimestamp(DateTime.UtcNow) - data.SavingTime);
            int livesCountToAdd = 0;
            while (timeLeft < 0)
            {
                timeLeft += LIVES_REFILL_INTERVAL;
                livesCountToAdd++;
            }
            _currentTimerInterval = timeLeft;
            LivesCount = data.LivesCount;
            AddLife(livesCountToAdd);
            _currentTimerInterval = LIVES_REFILL_INTERVAL;
        }

        private void OnTimerDestroy()
        {
            Save();
        }

        public static LivesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LivesManager();
                }
                return _instance;
            }
        }

        #endregion

        public const int MAX_LIVES = 10;
        private const float LIVES_REFILL_INTERVAL = 1200;
        private const string KEY = "countLives";

        private int _count;
        private readonly Timer _livesTimer;

        public int LivesCount
        {
            get { return _count; }
            private set
            {
                if (_count == value)
                {
                    return;
                }
                var prevCount = _count;
                _count = value;
                OnLivesCountChanged(_count, prevCount);
                Save();
            }
        }

        private void OnLivesCountChanged(int count, int prevCount)
        {
            if (count < MAX_LIVES && !_livesTimer.IsStarted) {
                _livesTimer.Interval = _currentTimerInterval;
                _livesTimer.StartTimer();
            }
            if (LivesCountChanged != null)
            {
                LivesCountChanged.Invoke(count, prevCount);
            }
        }

        public event Action<int, int> LivesCountChanged;

        public void AddLife(int count = 1)
        {
            LivesCount = Mathf.Clamp(LivesCount + count, 0, MAX_LIVES);
        }

        public void SpendLife(int count = 1)
        {
            LivesCount = Mathf.Clamp(LivesCount - count, 0, MAX_LIVES);
        }

        public TimeSpan TimeLeftToRefill
        {
            get { return _livesTimer.TimeLeft; }
        }

        private void OnTimer()
        {
            AddLife();
            //_livesTimer.StartTimer(LIVES_REFILL_INTERVAL);
        }

        private static DateTime _initialTime = new DateTime(1970, 1, 1);

        public static long GetTimestamp(DateTime datetime)
        {
            return ((long)(datetime - _initialTime).TotalSeconds);
        }

        private void Save()
        {
            var data = new LivesData(LivesCount, (int)_livesTimer.TimeLeft.TotalSeconds, GetTimestamp(DateTime.UtcNow));
            PlayerPrefs.SetString(KEY, data.ToJSON().ToString());
            PlayerPrefs.Save();
        }

        private LivesData Load()
        {
            return PlayerPrefs.HasKey(KEY) 
                ? LivesData.FromJson(new JSONObject(PlayerPrefs.GetString(KEY)))
                : new LivesData();
        }

        private class LivesData
        {
            public readonly int LivesCount;
            public readonly float TimeLeftValue;
            public readonly long SavingTime;

            public LivesData(int livesCount, float timeLeftValue, long savingTime)
            {
                LivesCount = livesCount;
                TimeLeftValue = timeLeftValue;
                SavingTime = savingTime;
            }

            public LivesData()
            {
                LivesCount = MAX_LIVES;
                TimeLeftValue = LIVES_REFILL_INTERVAL;
                SavingTime = GetTimestamp(DateTime.UtcNow);
            }

            public JSONObject ToJSON()
            {
                var json = new JSONObject();
                json.AddField("count", LivesCount);
                json.AddField("timer", TimeLeftValue);
                json.AddField("time", SavingTime.ToString());
                return json;
            }

            public static LivesData FromJson(JSONObject json)
            {
                var lives = int.Parse(json["count"].ToString().Trim('\"'));
                var timer = float.Parse(json["timer"].ToString().Trim('\"'));
                var time = long.Parse(json["time"].ToString().Trim('\"'));
                return new LivesData(lives, timer, time);
            }
        }
    }
}
