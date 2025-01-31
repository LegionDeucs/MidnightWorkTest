using UnityEngine;

public static partial class Extensions
{
    public static int Remap(this int value, int oldMin, int oldMax, int newMin, int newMax)
    {
        float t = Mathf.InverseLerp(oldMin, oldMax, value);
        value = (int)Mathf.Lerp(newMin, newMax, t);
        return value;
    }
    
    public static float Remap(this float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        float t = Mathf.InverseLerp(oldMin, oldMax, value);
        value = Mathf.Lerp(newMin, newMax, t);
        return value;
    }
}