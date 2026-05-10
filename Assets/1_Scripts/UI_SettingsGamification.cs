using UnityEngine.UI;
using UnityEngine;

public class UI_SettingsGamification : MonoBehaviour
{
    private Toggle _toggle;

    public const string GAMIFICATION_TOGGLE = "GamificationToggle";

    private void OnEnable()
    {
        var value = PlayerPrefs.GetInt(GAMIFICATION_TOGGLE, 0);
        _toggle.isOn = value == 1;
    }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ToggleUI);
    }

    private void ToggleUI(bool state)
    {
        UI_Progress.Instance.gameObject.SetActive(!state);
        PlayerPrefs.SetInt(GAMIFICATION_TOGGLE, state ? 1 : 0);
    }
}
