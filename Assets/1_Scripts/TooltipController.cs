using UnityEngine.InputSystem;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public static TooltipController Instance { get; private set; }

    [SerializeField] private UI_Tooltip _tooltip;
    [SerializeField] private Vector2 _offset;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _tooltip.transform.position = Mouse.current.position.ReadValue() + _offset;
    }

    public void ShowTooltip(CharacterPair pair)
    {
        _tooltip.Show(pair);
    }

    public void HideTooltip()
    {
        _tooltip.Hide();
    }
}
