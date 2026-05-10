using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

public class UI_Hint : MonoBehaviour
{
    public static UI_Hint Instance { get; private set; }

    [SerializeField] private Transform _container;
    [SerializeField] private UI_HintSlot _slotTemplate;
    [SerializeField] private TextMeshProUGUI _hintButtonLabelTMP;

    private List<UI_HintSlot> _slots = new();

    private void Awake()
    {
        Instance = this;
        _slotTemplate.gameObject.SetActive(false);

        ToggleUI();
    }

    private void Start()
    {
        KanaController.Instance.OnSwitchedGeneralKana += KanaController_OnSwitchedGeneralKana;

        HashSet<CharacterPair> pairList = new();
        for (int i = 0; i < Enum.GetValues(typeof(KanaGroup)).Length; i++)
        {
            var kanaGroup = (KanaGroup)i;
            var hiraganaSeion = KanaManager.Instance.Kana.hiragana.seion.characterLists.GetList(kanaGroup);
            var hiraganaDakuon = KanaManager.Instance.Kana.hiragana.dakuon.characterLists.GetList(kanaGroup);
            var hiraganaHandakon = KanaManager.Instance.Kana.hiragana.handakuon.characterLists.GetList(kanaGroup);
            var hiraganaYouon = KanaManager.Instance.Kana.hiragana.youon.characterLists.GetList(kanaGroup);

            var katakanaSeion = KanaManager.Instance.Kana.katakana.seion.characterLists.GetList(kanaGroup);
            var katakanaDakuon = KanaManager.Instance.Kana.katakana.dakuon.characterLists.GetList(kanaGroup);
            var katakanaHandakon = KanaManager.Instance.Kana.katakana.handakuon.characterLists.GetList(kanaGroup);
            var katakanaYouon = KanaManager.Instance.Kana.katakana.youon.characterLists.GetList(kanaGroup);

            if (hiraganaSeion != null) pairList.AddRange(hiraganaSeion.list);
            if (hiraganaDakuon != null) pairList.AddRange(hiraganaDakuon.list);
            if (hiraganaHandakon != null) pairList.AddRange(hiraganaHandakon.list);
            if (hiraganaYouon != null) pairList.AddRange(hiraganaYouon.list);

            if (katakanaSeion != null) pairList.AddRange(katakanaSeion.list);
            if (katakanaDakuon != null) pairList.AddRange(katakanaDakuon.list);
            if (katakanaHandakon != null) pairList.AddRange(katakanaHandakon.list);
            if (katakanaYouon != null) pairList.AddRange(katakanaYouon.list);
        }

        foreach (var kana in pairList)
        {
            var slot = Instantiate(_slotTemplate, _container);
            slot.Init(kana);
            slot.Highlight(kana == KanaController.Instance.CurrentCharacterPair);
            _slots.Add(slot);
        }
    }

    private void KanaController_OnSwitchedGeneralKana()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var slot in _slots)
        {
            slot.gameObject.SetActive(false);
        }

        foreach (var kanaList in KanaController.Instance.KanaPool)
        {
            foreach (var kana in kanaList.list)
            {
                var match = _slots.FirstOrDefault(e => e.CharacterPair == kana);
                if (match != null)
                {
                    match.gameObject.SetActive(true);
                    match.ResetUI();
                    var shouldHighlight = kana == KanaController.Instance.CurrentCharacterPair;
                    match.Highlight(shouldHighlight);
                }
            }
        }
    }

    public void ToggleUI()
    {
        _container.gameObject.SetActive(!_container.gameObject.activeSelf);

        if (_container.gameObject.activeSelf)
            UpdateUI();

        _hintButtonLabelTMP.text = $"Vorschau\n" +
            $"{(_container.gameObject.activeSelf ? "aktiv" : "inaktiv")}";
    }
}
