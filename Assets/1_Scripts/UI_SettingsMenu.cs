using UnityEngine.UI;
using UnityEngine;
using MoreMountains.Feedbacks;

public class UI_SettingsMenu : MonoBehaviour
{
    public static UI_SettingsMenu Instance { get; private set; }

    [SerializeField] private GameObject _container;

    [SerializeField] private MMF_Player _showFeedback;
    [SerializeField] private MMF_Player _hideFeedback;

    [SerializeField] private Slider _hue;
    [SerializeField] private Slider _saturation;
    [SerializeField] private Slider _bright;
    [SerializeField] private Slider _pixelate;
    [SerializeField] private Slider _waveAmount;
    [SerializeField] private Slider _waveSpeed;
    [SerializeField] private Slider _waveStrength;
    [SerializeField] private Slider _roundWaveStrength;
    [SerializeField] private Slider _roundWaveSpeed;

    [SerializeField] private Toggle _gamificationToggle;
    [SerializeField] private Toggle _soundToggle;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI(float hue, float saturation, float bright, float pixelateSize, float waveAmount, float waveSpeed, float waveStrength, float roundWaveStrength, float roundWaveSpeed)
    {
        _hue.value = hue;
        _saturation.value = saturation;
        _bright.value = bright;

        _pixelate.value = pixelateSize;

        _waveAmount.value = waveAmount;
        _waveSpeed.value = waveSpeed;
        _waveStrength.value = waveStrength;

        _roundWaveStrength.value = roundWaveStrength;
        _roundWaveSpeed.value = roundWaveSpeed;
    }

    public void Toggle()
    {
        if (_container.activeSelf)
            _hideFeedback.PlayFeedbacks();
        else
            _showFeedback.PlayFeedbacks();
    }
}
