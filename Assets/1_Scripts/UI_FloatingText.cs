using UnityEngine;
using TMPro;

public class UI_FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;

    public void SetText(string text)
    {
        _tmp.text = text;
    }
}
