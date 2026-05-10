using UnityEngine;
using System;

public class ProgressController : MonoBehaviour
{
    public static ProgressController Instance { get; private set; }

    public event Action<OnProgressArgs> OnProgress;
    public class OnProgressArgs : EventArgs
    {
        public float xpGain;
    }

    [SerializeField] private float _baseXPGain;
    [SerializeField] private float _baseMaxXP;
    [SerializeField] private float _maxXPIncPercentage;

    public float CurrentXP { get; private set; }
    public float MaxXP { get; private set; }
    public float CurrentLevel { get; private set; }
    public float StreakMultiplier { get; private set; }

    private float _totalXP;
    private float _timer;
    private bool _startTimer;
    private const string PLAYER_PREFS_TOTAL_XP = "TOTAL_XP";

    private void Awake()
    {
        Instance = this;
        MaxXP = _baseMaxXP;
    }

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (_startTimer)
            _timer += Time.deltaTime;
    }

    public void GainXP(CharacterPair pair)
    {
        _startTimer = true;

        UpdateStreak();

        float timerBonusMult = 1f;
        if (_timer < 2f)
            timerBonusMult = 2f;
        else if (_timer < 3f)
            timerBonusMult = 1.5f;

        _timer = 0f;

        var xpGain = timerBonusMult * StreakMultiplier * _baseXPGain * Stats.GetData(pair).CalcCorrectPercentage();
        var xpNeeded = MaxXP - CurrentXP;
        var overshoot = xpGain - xpNeeded;

        _totalXP += xpGain;

        if (overshoot >= 0)
        {
            CurrentLevel++;
            CurrentXP = overshoot;
            MaxXP *= _maxXPIncPercentage;
        }
        else
        {
            CurrentXP += xpGain;
        }

        OnProgress?.Invoke(new OnProgressArgs { xpGain = xpGain });

        Save();
    }

    public void UpdateStreak()
    {
        StreakMultiplier = Mathf.Clamp(1f + Stats.HighestStreak * 0.1f, 1f, 10f);
        UI_Progress.Instance.UpdateUI();
    }

    private void InitLevel()
    {
        float a = _baseMaxXP;
        float r = _maxXPIncPercentage;
        float xp = _totalXP;

        float level = Mathf.Log((xp * (r - 1f)) / a + 1f) / Mathf.Log(r);

        CurrentLevel = Mathf.Floor(level);
        float xpUsedForLevels = a * (Mathf.Pow(r, CurrentLevel) - 1f) / (r - 1f);

        CurrentXP = xp - xpUsedForLevels;
        MaxXP = a * Mathf.Pow(r, CurrentLevel);
    }

    public void ResetProgress()
    {
        CurrentLevel = 0;
        CurrentXP = 0;
        MaxXP = _baseMaxXP;
        _timer = 0f;
        _totalXP = 0f;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(PLAYER_PREFS_TOTAL_XP, _totalXP);
    }

    private void Load()
    {
        _totalXP = PlayerPrefs.GetFloat(PLAYER_PREFS_TOTAL_XP, 0f);
        InitLevel();
        OnProgress?.Invoke(new OnProgressArgs { xpGain = 0f });
    }
}
