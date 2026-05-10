using MoreMountains.Tools;
using UnityEngine.UI;
using UnityEngine;

public class UI_SettingsSound : MonoBehaviour
{
    private Toggle _toggle;

    private const string SOUND_TOGGLE = "SoundToggle";

    private void OnEnable()
    {
        var value = PlayerPrefs.GetInt(SOUND_TOGGLE, 1);
        _toggle.isOn = value == 1;
    }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ToggleUI);
    }

    private void ToggleUI(bool state)
    {
        if (!state)
            MMSoundManager.Instance.MuteSfx();
        else
            MMSoundManager.Instance.UnmuteSfx();

        PlayerPrefs.SetInt(SOUND_TOGGLE, state ? 1 : 0);
    }
}
