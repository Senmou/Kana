using MoreMountains.Feedbacks;
using UnityEngine;
using TMPro;

public class UI_Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TextMeshProUGUI _correctTMP;
    [SerializeField] private TextMeshProUGUI _wrongTMP;
    [SerializeField] private TextMeshProUGUI _percentageTMP;
    [SerializeField] private TextMeshProUGUI _kanaTMP;
    [SerializeField] private TextMeshProUGUI _romajiTMP;
    [SerializeField] private MMF_Player _showFeedback;

    public void Show(CharacterPair pair)
    {
        _container.SetActive(true);
        _showFeedback.PlayFeedbacks();

        var data = Stats.GetData(pair);

        var correct = 0;
        var wrong = 0;
        var percentage = 0f;

        if (data != null)
        {
            correct = data.correct;
            wrong = data.wrong;
            percentage = data.CalcCorrectPercentage();
        }

        _correctTMP.text = $"{correct:0}";
        _wrongTMP.text = $"{wrong:0}";
        _percentageTMP.text = $"{percentage:0%}";
        _kanaTMP.text = pair.kana;
        _romajiTMP.text = pair.romaji;
    }

    public void Hide()
    {
        _container.SetActive(false);
    }
}
