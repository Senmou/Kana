using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI_KanaSwitchToggle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _labelTMP;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ToggleGeneralKana);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void ToggleGeneralKana(bool state)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (KanaController.Instance.GeneralKana == GeneralKana.Hiragana)
        {
            KanaController.Instance.SetGeneralKana(GeneralKana.Katakana);
            _labelTMP.text = "Katakana\n" +
                "カタカナ";
        }
        else
        {
            KanaController.Instance.SetGeneralKana(GeneralKana.Hiragana);
            _labelTMP.text = "Hiragana\n" +
                "ひらがな";
        }
    }
}
