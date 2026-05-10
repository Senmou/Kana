using UnityEngine;

public static class Helper
{
    public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
    {
        var t = Mathf.InverseLerp(iMin, iMax, value);
        return Mathf.Lerp(oMin, oMax, t);
    }
}
