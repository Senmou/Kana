using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Toggle))]
public class KanaToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private KanaGroup _kanaGroup;
    [SerializeField] private KanaType _kanaType;
    [SerializeField] private MMF_Player _hoverFeedback;
    [SerializeField] private MMF_Player _stopHoverFeedback;
    [SerializeField] private MMF_Player _clickFeedback;

    private Toggle _toggle;
    private CharacterList _characterList;
    private UI_ToggleGroup _toggleGroup;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(Toggle_OnValueChanged);
        _toggleGroup = GetComponentInParent<UI_ToggleGroup>();
    }

    private void Start()
    {
        KanaController.Instance.OnSwitchedGeneralKana += KanaController_OnSwitchedGeneralKana;
        UpdateCharacterList();
        _toggle.isOn = false;
    }

    private void KanaController_OnSwitchedGeneralKana()
    {
        KanaController.Instance.RemoveFromKanaPool(_characterList);

        UpdateCharacterList();

        if (_toggle.isOn)
            KanaController.Instance.AddToKanaPool(_characterList);
    }

    private void Toggle_OnValueChanged(bool state)
    {
        OnToggleValueChanged(state, shouldGenerateNewRandomKana: true);
    }

    public void OnToggleValueChanged(bool state, bool shouldGenerateNewRandomKana)
    {
        if (state)
        {
            KanaController.Instance.AddToKanaPool(_characterList);
            if (_toggleGroup != null)
            {
                if (_toggleGroup.AreAllOn())
                    _toggleGroup.GroupToggle.Toggle.SetIsOnWithoutNotify(true);
            }
        }
        else
        {
            KanaController.Instance.RemoveFromKanaPool(_characterList);

            if (_toggleGroup != null)
                _toggleGroup.GroupToggle.Toggle.SetIsOnWithoutNotify(false);
        }

        _toggleGroup.GroupToggle.UpdateUI();

        if (shouldGenerateNewRandomKana)
            KanaController.Instance.InitRandomKana();
    }

    private void UpdateCharacterList()
    {
        _characterList = KanaManager.Instance.GetCharacterList(_kanaGroup, _kanaType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _stopHoverFeedback.StopFeedbacks();
        _hoverFeedback.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverFeedback.StopFeedbacks();
        _stopHoverFeedback.PlayFeedbacks();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _clickFeedback.PlayFeedbacks();
    }
}
