using UnityEngine;

[System.Serializable]
public class ColorSerializable
{
    public float r;
    public float g;
    public float b;
    public float a;

    public ColorSerializable(Color color)
    {
        r = color.a;
        g = color.g;
        b = color.b;
        a = color.a;
    }

    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }
}