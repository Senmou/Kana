using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class UI_ToggleGroup : MonoBehaviour
{
    [field: SerializeField] public UI_GroupToggle GroupToggle { get; private set; }

    private List<Toggle> _toggles;

    private void Awake()
    {
        _toggles = GetComponentsInChildren<Toggle>().ToList();
    }

    public void SetStateForGroup(bool state)
    {
        for (int i = 0; i < _toggles.Count; i++)
        {
            var toggle = _toggles[i];

            if (toggle.isOn == state)
                continue;

            toggle.SetIsOnWithoutNotify(state);

            if (toggle.TryGetComponent(out UI_Toggle ui_toggle))
                ui_toggle.UpdateUI();

            if (toggle.TryGetComponent(out KanaToggle kanaToggle))
                kanaToggle.OnToggleValueChanged(state, shouldGenerateNewRandomKana: i == _toggles.Count - 1);
        }
    }

    public bool AreAllOn()
    {
        foreach (var toggle in _toggles)
        {
            if (!toggle.isOn)
                return false;
        }
        return true;
    }
}
