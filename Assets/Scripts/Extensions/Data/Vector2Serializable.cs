using UnityEngine;

[System.Serializable]
public class Vector2Serializable
{
    public float x;
    public float y;

    public Vector2Serializable(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }

    public Vector2 ToVector3()
    {
        return new Vector2(x, y);
    }
}