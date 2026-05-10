using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI_HintSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _kanaTMP;
    [SerializeField] private TextMeshProUGUI _romajiTMP;
    [SerializeField] private Image _background;
    [SerializeField] private Canvas _slotCanvas;
    [SerializeField] private MMF_Player _highlightFeedback;
    [SerializeField] private MMF_Player _resetFeedback;

    public CharacterPair CharacterPair { get; private set; }

    private bool _isHighlighted;

    private void Start()
    {
        KanaController.Instance.OnNewKana += KanaController_OnNewKana;
    }

    private void KanaController_OnNewKana()
    {
        Highlight(KanaController.Instance.CurrentCharacterPair == CharacterPair);
    }

    public void Init(CharacterPair pair)
    {
        CharacterPair = pair;
        _kanaTMP.text = pair.kana;
        _romajiTMP.text = pair.romaji;
    }

    public void Highlight(bool state)
    {
        if (_isHighlighted && !state)
            _resetFeedback.PlayFeedbacks();
        else if (!_isHighlighted && state)
            _highlightFeedback.PlayFeedbacks();

        _isHighlighted = state;

        _kanaTMP.color = state ? Color.green : Color.white;
        _romajiTMP.color = state ? Color.green : Color.white;
        _slotCanvas.sortingOrder = state ? 200 : 25;
    }

    public void ResetUI()
    {
        _isHighlighted = false;
        _background.gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipController.Instance.ShowTooltip(CharacterPair);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();
    }
}
