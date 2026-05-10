using static ProgressController;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI_Progress : MonoBehaviour
{
    public static UI_Progress Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _currentXP;
    [SerializeField] private TextMeshProUGUI _maxXP;
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private TextMeshProUGUI _streakValue;
    [SerializeField] private TextMeshProUGUI _streakXPMultiplier;
    [SerializeField] private UI_FloatingText _floatingTextPrefab;
    [SerializeField] private Transform _floatingTextSpawnContainer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ProgressController.Instance.OnProgress += ProgressController_OnProgress;
        UpdateUI();

        var value = PlayerPrefs.GetInt(UI_SettingsGamification.GAMIFICATION_TOGGLE, 0);
        gameObject.SetActive(value == 0);
    }

    private void ProgressController_OnProgress(OnProgressArgs onProgressArgs)
    {
        UpdateUI();

        if (gameObject.activeSelf && onProgressArgs.xpGain != 0f)
        {
            var floatingText = Instantiate(_floatingTextPrefab, _floatingTextSpawnContainer.position, Quaternion.identity, _floatingTextSpawnContainer);
            floatingText.SetText($"+{onProgressArgs.xpGain:0 XP}");
        }
    }

    public void UpdateUI()
    {
        _currentXP.text = $"{ProgressController.Instance.CurrentXP:0} XP";
        _maxXP.text = $"{ProgressController.Instance.MaxXP:0} XP";
        _currentLevel.text = $"Lv {ProgressController.Instance.CurrentLevel:0}";

        var xpRatio = ProgressController.Instance.CurrentXP / ProgressController.Instance.MaxXP;
        _xpSlider.value = xpRatio;

        _streakValue.text = $"{Stats.HighestStreak}";
        _streakValue.fontSize = Helper.Remap(0f, 100f, 100f, 250f, Stats.HighestStreak);
        _streakXPMultiplier.text = $"XP x{ProgressController.Instance.StreakMultiplier:0.00}";
    }
}
