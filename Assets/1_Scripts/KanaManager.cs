using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System;

public enum KanaGroup
{
    K,
    S,
    T,
    N,
    H,
    M,
    Y,
    R,
    W,
    G,
    Z,
    D,
    B,
    P,
    Vocals,
    KY,
    SH,
    CH,
    NY,
    HY,
    MY,
    RY,
    GY,
    J,
    DY,
    BY,
    PY
}

public enum KanaType
{
    Seion,
    Dakuon,
    Handakuon,
    Youon
}

public class KanaManager : MonoBehaviour
{
    public static KanaManager Instance { get; private set; }

    public Kana Kana { get; private set; }

    private void Awake()
    {
        Instance = this;
        ProcessJson();
    }

    private void ProcessJson()
    {
        var textAsset = Resources.Load<TextAsset>("Kana");
        Kana = JsonConvert.DeserializeObject<Kana>(textAsset.text);
    }

    public CharacterList GetCharacterList(KanaGroup group, KanaType type)
    {
        if (KanaController.Instance.GeneralKana == GeneralKana.Hiragana)
        {
            switch (type)
            {
                case KanaType.Seion:
                    return Kana.hiragana.seion.characterLists.GetList(group);
                case KanaType.Dakuon:
                    return Kana.hiragana.dakuon.characterLists.GetList(group);
                case KanaType.Handakuon:
                    return Kana.hiragana.handakuon.characterLists.GetList(group);
                case KanaType.Youon:
                    return Kana.hiragana.youon.characterLists.GetList(group);
                default:
                    {
                        Debug.LogWarning($"{group} {type} CharacterList not found");
                        return null;
                    }
            }
        }
        else
        {
            switch (type)
            {
                case KanaType.Seion:
                    return Kana.katakana.seion.characterLists.GetList(group);
                case KanaType.Dakuon:
                    return Kana.katakana.dakuon.characterLists.GetList(group);
                case KanaType.Handakuon:
                    return Kana.katakana.handakuon.characterLists.GetList(group);
                case KanaType.Youon:
                    return Kana.katakana.youon.characterLists.GetList(group);
                default:
                    {
                        Debug.LogWarning($"{group} {type} CharacterList not found");
                        return null;
                    }
            }
        }
    }
}

[Serializable]
public class Kana
{
    public Hiragana hiragana;
    public Katakana katakana;
}

[Serializable]
public class Hiragana
{
    public Seion seion;
    public Dakuon dakuon;
    public Handakuon handakuon;
    public Youon youon;
}

[Serializable]
public class Katakana
{
    public Seion seion;
    public Dakuon dakuon;
    public Handakuon handakuon;
    public Youon youon;
}

[Serializable]
public class Seion
{
    public CharacterLists characterLists;
}

[Serializable]
public class Dakuon
{
    public CharacterLists characterLists;
}

[Serializable]
public class Handakuon
{
    public CharacterLists characterLists;
}

[Serializable]
public class Youon
{
    public CharacterLists characterLists;
}

[Serializable]
public class CharacterLists
{
    public CharacterList vocals;
    public CharacterList k;
    public CharacterList s;
    public CharacterList t;
    public CharacterList n;
    public CharacterList h;
    public CharacterList m;
    public CharacterList y;
    public CharacterList r;
    public CharacterList w;
    public CharacterList g;
    public CharacterList z;
    public CharacterList d;
    public CharacterList b;
    public CharacterList p;
    public CharacterList ky;
    public CharacterList sh;
    public CharacterList ch;
    public CharacterList ny;
    public CharacterList hy;
    public CharacterList my;
    public CharacterList ry;
    public CharacterList gy;
    public CharacterList j;
    public CharacterList dy;
    public CharacterList by;
    public CharacterList py;

    public CharacterList GetList(KanaGroup group)
    {
        return group switch
        {
            KanaGroup.K => k,
            KanaGroup.S => s,
            KanaGroup.T => t,
            KanaGroup.N => n,
            KanaGroup.H => h,
            KanaGroup.M => m,
            KanaGroup.Y => y,
            KanaGroup.R => r,
            KanaGroup.W => w,
            KanaGroup.G => g,
            KanaGroup.Z => z,
            KanaGroup.D => d,
            KanaGroup.B => b,
            KanaGroup.P => p,
            KanaGroup.Vocals => vocals,
            KanaGroup.KY => ky,
            KanaGroup.SH => sh,
            KanaGroup.CH => ch,
            KanaGroup.NY => ny,
            KanaGroup.HY => hy,
            KanaGroup.MY => my,
            KanaGroup.RY => ry,
            KanaGroup.GY => gy,
            KanaGroup.J => j,
            KanaGroup.DY => dy,
            KanaGroup.BY => by,
            KanaGroup.PY => py,
            _ => null,
        };
    }
}

[Serializable]
public class CharacterList
{
    public List<CharacterPair> list;
}

[Serializable]
public class CharacterPair
{
    public string kana;
    public string romaji;
}