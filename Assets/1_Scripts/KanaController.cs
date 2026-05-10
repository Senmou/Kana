using System.Collections.Generic;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

public class KanaLoot : MMLoot<CharacterPair>
{

}

public class KanaLDT : MMLootTable<KanaLoot, CharacterPair>
{

}

public enum GeneralKana
{
    Hiragana,
    Katakana
}
//Sound Effect by <a href="https://pixabay.com/users/xmersounds-50703818/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=416828">Xmer™ Sounds</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=416828">Pixabay</a>
public class KanaController : MonoBehaviour
{
    public static KanaController Instance { get; private set; }

    public event Action OnNewKana;
    public event Action OnSwitchedGeneralKana;

    [SerializeField] private TextMeshProUGUI _kanaTMP;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private AnimationCurve _weightModifierCurve;
    [SerializeField] private float _weightModfier;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player _kanaFeedback;
    [SerializeField] private MMF_Player _inputFieldFeedback;
    [SerializeField] private MMF_Player _successFeedback;

    [TextArea(20, 20)]
    public string debugText;

    public List<CharacterList> KanaPool => _kanaPool;
    public CharacterPair CurrentCharacterPair => _currentCharacterPair;
    public GeneralKana GeneralKana { get; private set; }

    private CharacterPair _currentCharacterPair;
    private readonly List<CharacterList> _kanaPool = new();
    private KanaLDT _kanaLDT = new();

    private void Awake()
    {
        Instance = this;
        _kanaLDT.ObjectsToLoot = new();
        GeneralKana = GeneralKana.Hiragana;
    }

    private void Start()
    {
        _inputField.Select();

        InitRandomKana();
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (_currentCharacterPair == null)
                return;

            if (_inputField.text == _currentCharacterPair.romaji)
                HandleCorrectGuess();
            else
                HandleWrongGuess();

            debugText = string.Empty;
            foreach (var loot in _kanaLDT.ObjectsToLoot)
            {
                var pair = loot.Loot;
                debugText += $"{pair.kana} - {loot.ChancePercentage:0.0}%\n";
            }
        }
    }

    public void SetGeneralKana(GeneralKana genralKana)
    {
        GeneralKana = genralKana;
        OnSwitchedGeneralKana?.Invoke();
        InitRandomKana();
    }

    private void HandleCorrectGuess()
    {
        Stats.UpdateDict(_currentCharacterPair, true);
        ProgressController.Instance.GainXP(_currentCharacterPair);

        var lootCount = _kanaLDT.ObjectsToLoot.Count;
        var weight = 0.2f * lootCount * _weightModfier * _weightModifierCurve.Evaluate(Mathf.Pow(Stats.GetData(_currentCharacterPair).CalcCorrectPercentage(), 2f));

        AddWeight(_currentCharacterPair, -(int)weight);

        InitRandomKana();
        _inputField.text = string.Empty;
        _inputField.ActivateInputField();
        UpdateUI();

        _successFeedback.PlayFeedbacks();
    }

    private void HandleWrongGuess()
    {
        Stats.UpdateDict(_currentCharacterPair, false);
        _inputFieldFeedback.PlayFeedbacks();
        ProgressController.Instance.UpdateStreak();

        var lootCount = _kanaLDT.ObjectsToLoot.Count;
        var weight = 0.2f * lootCount * _weightModfier * (1f - _weightModifierCurve.Evaluate(Mathf.Pow(Stats.GetData(_currentCharacterPair).CalcCorrectPercentage(), 2f)));
        AddWeight(_currentCharacterPair, (int)weight);

        _inputField.text = string.Empty;
        _inputField.ActivateInputField();
    }

    private void UpdateKanaLDT()
    {
        _kanaLDT.ObjectsToLoot.Clear();

        foreach (var kanaList in _kanaPool)
        {
            if (kanaList == null)
                continue;

            foreach (var pair in kanaList.list)
            {
                _kanaLDT.ObjectsToLoot.Add(new KanaLoot() { Loot = pair, Weight = 0 });
            }
        }
        var lootCount = _kanaLDT.ObjectsToLoot.Count;
        foreach (var loot in _kanaLDT.ObjectsToLoot)
        {
            loot.Weight = 20 * lootCount;
        }

        _kanaLDT.ComputeWeights();
    }

    private void AddWeight(CharacterPair pair, int weight)
    {
        var loot = _kanaLDT.ObjectsToLoot.FirstOrDefault(e => e.Loot == pair);
        if (loot == null) return;

        var lootCount = _kanaLDT.ObjectsToLoot.Count;

        loot.Weight += weight;
        loot.Weight = Math.Clamp(loot.Weight, 10, 40 * lootCount);
        _kanaLDT.ComputeWeights();
    }

    public void AddToKanaPool(CharacterList list)
    {
        _kanaPool.Add(list);
        UpdateKanaLDT();
    }

    public void RemoveFromKanaPool(CharacterList list)
    {
        _kanaPool.Remove(list);
        UpdateKanaLDT();
    }

    public void InitRandomKana()
    {
        var currentLoot = _kanaLDT.ObjectsToLoot.FirstOrDefault(e => e.Loot == _currentCharacterPair);

        float lastLootWeight = 0f;
        if (currentLoot != null)
        {
            lastLootWeight = currentLoot.Weight;
            currentLoot.Weight = 0;
            _kanaLDT.ComputeWeights();
        }

        var loot = _kanaLDT.GetLoot();

        if (currentLoot != null)
        {
            currentLoot.Weight = lastLootWeight;
            _kanaLDT.ComputeWeights();
        }

        if (loot == null)
        {
            UI_Hint.Instance.UpdateUI();
            _kanaTMP.text = string.Empty;

            return;
        }

        _currentCharacterPair = loot.Loot;

        UpdateUI();

        UI_Hint.Instance.UpdateUI();
        OnNewKana?.Invoke();

        _kanaFeedback.PlayFeedbacks();
    }

    private void UpdateUI()
    {
        if (_currentCharacterPair == null)
            return;

        _kanaTMP.text = $"{_currentCharacterPair.kana}";
    }
}
