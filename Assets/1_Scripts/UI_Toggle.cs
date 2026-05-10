using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Toggle))]
public class UI_Toggle : MonoBehaviour
{
    [SerializeField] private Image _background;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(Toggle_OnValueChanged);
    }

    private void Toggle_OnValueChanged(bool state)
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _background.color = _toggle.isOn ? _background.color.WithA(1f) : _background.color.WithA(0.15f);
    }
}
