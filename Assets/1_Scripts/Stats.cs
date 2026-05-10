using System.Collections.Generic;

public static class Stats
{
    private static Dictionary<CharacterPair, KanaData> _data = new();
    public static int HighestStreak { get; private set; }

    public static void UpdateDict(CharacterPair pair, bool correct)
    {
        if (!_data.ContainsKey(pair))
        {
            var correctCount = correct ? 1 : 0;
            var wrongCount = correct ? 0 : 1;
            var data = new KanaData { pair = pair, correct = correctCount, wrong = wrongCount };
            _data.Add(pair, data);
        }
        else
        {
            var data = _data[pair];
            data.correct += correct ? 1 : 0;
            data.wrong += correct ? 0 : 1;
            _data[pair] = data;
        }

        if (correct)
            HighestStreak++;
        else
            HighestStreak = 0;
    }

    public static KanaData GetData(CharacterPair pair)
    {
        if (_data.ContainsKey(pair))
            return _data[pair];
        else
            return null;
    }

    public static void ResetStats()
    {
        _data = new();
        HighestStreak = 0;
    }
}

public class KanaData
{
    public CharacterPair pair;
    public int correct;
    public int wrong;

    public float CalcCorrectPercentage()
    {
        if (correct == 0 && wrong == 0) return 0f;
        return (float)correct / (correct + wrong);
    }
}
