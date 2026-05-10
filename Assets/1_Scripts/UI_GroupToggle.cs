using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Toggle))]
public class UI_GroupToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UI_ToggleGroup _toggleGroup;
    [SerializeField] private MMF_Player _clickFeedback;
    [SerializeField] private MMF_Player _hoverFeedback;
    [SerializeField] private MMF_Player _stopHoverFeedback;

    public Toggle Toggle { get; private set; }

    private UI_Toggle _uiToggle;

    private void Awake()
    {
        _uiToggle = GetComponent<UI_Toggle>();
        Toggle = GetComponent<Toggle>();
        Toggle.onValueChanged.AddListener(Toggle_OnValueChanged);
    }

    private void Start()
    {
        Toggle.SetIsOnWithoutNotify(false);
    }

    private void Toggle_OnValueChanged(bool state)
    {
        _clickFeedback.PlayFeedbacks();
        if (_toggleGroup.AreAllOn())
        {
            _toggleGroup.SetStateForGroup(false);
        }
        else
        {
            _toggleGroup.SetStateForGroup(true);
        }
    }

    public void OnActivateToggle()
    {
        if (_toggleGroup.AreAllOn())
        {
            _toggleGroup.SetStateForGroup(false);
        }
        else
        {
            _toggleGroup.SetStateForGroup(true);
        }
    }

    public void UpdateUI()
    {
        _uiToggle.UpdateUI();
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
}
