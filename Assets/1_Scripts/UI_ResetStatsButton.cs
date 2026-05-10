using UnityEngine.UI;
using UnityEngine;

public class UI_ResetStatsButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Stats.ResetStats();

        ProgressController.Instance.ResetProgress();
        UI_Progress.Instance.UpdateUI();
    }
}
