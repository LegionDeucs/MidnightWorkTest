using UnityEngine;

public static partial class Extensions
{
    public static Vector2 Abs(this Vector2 vector)
    {
        var absVector = new Vector2();
        absVector.x = Mathf.Abs(vector.x);
        absVector.y = Mathf.Abs(vector.y);
        return absVector;
    }

    public static Vector2Int FloorToInt(this Vector2 vector)
    {
        var v2i = new Vector2Int();
        v2i.x = Mathf.FloorToInt(vector.x);
        v2i.y = Mathf.FloorToInt(vector.y);
        return v2i;
    }

    public static Vector2Int CeilToInt(this Vector2 vector)
    {
        var v2i = new Vector2Int();
        v2i.x = Mathf.CeilToInt(vector.x);
        v2i.y = Mathf.CeilToInt(vector.y);
        return v2i;
    }

    public static Vector2Int RoundToInt(this Vector2 vector)
    {
        var v2i = new Vector2Int();
        v2i.x = Mathf.RoundToInt(vector.x);
        v2i.y = Mathf.RoundToInt(vector.y);
        return v2i;
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        var absVector = new Vector3();
        absVector.x = Mathf.Abs(vector.x);
        absVector.y = Mathf.Abs(vector.y);
        absVector.z = Mathf.Abs(vector.z);
        return absVector;
    }

    public static Vector3Int FloorToInt(this Vector3 vector)
    {
        var v3i = new Vector3Int();
        v3i.x = Mathf.FloorToInt(vector.x);
        v3i.y = Mathf.FloorToInt(vector.y);
        v3i.z = Mathf.FloorToInt(vector.z);
        return v3i;
    }

    public static Vector3Int CeilToInt(this Vector3 vector)
    {
        var v3i = new Vector3Int();
        v3i.x = Mathf.CeilToInt(vector.x);
        v3i.y = Mathf.CeilToInt(vector.y);
        v3i.z = Mathf.CeilToInt(vector.z);
        return v3i;
    }

    public static Vector3Int RoundToInt(this Vector3 vector)
    {
        var v3i = new Vector3Int();
        v3i.x = Mathf.RoundToInt(vector.x);
        v3i.y = Mathf.RoundToInt(vector.y);
        v3i.z = Mathf.RoundToInt(vector.z);
        return v3i;
    }

    public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);

    public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);

    public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

    public static Vector3 AddX(this Vector3 vector, float x) => new Vector3(vector.x + x, vector.y, vector.z);
    
    public static Vector3 AddY(this Vector3 vector, float y) => new Vector3(vector.x, vector.y + y, vector.z);

    public static Vector3 AddZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, vector.z + z);

    public static Vector3 Add(this Vector3 vector, Vector3 amount) => new Vector3(vector.x + amount.x, vector.y + amount.y, vector.z + amount.z);

    public static Vector3 XVector(this Vector3 vector) => new Vector3(vector.x, 0, 0);
    public static Vector3 YVector(this Vector3 vector) => new Vector3(0, vector.y, 0);
    public static Vector3 ZVector(this Vector3 vector) => new Vector3(0, 0, vector.z);

    public static Vector3 YtoZ(this Vector2 vector) => new Vector3(vector.x, 0f, vector.y);

    public static Vector3 ZtoY(this Vector3 vector) => new Vector2(vector.x, vector.z);

    public static Vector3 Multiply(this Vector3 vector, Vector3 amount) => new Vector3(vector.x * amount.x, vector.y*amount.y, vector.z * amount.z);

    public static float GetRandom(this Vector2 vector)
    {
        return Random.Range(vector.x, vector.y);
    }

    public static Vector3Serializable ToVector3Serializable(this Vector3 vector3)
    {
        return new Vector3Serializable(vector3);
    }
    
    public static Vector2Serializable ToVector2Serializable(this Vector2 vector2)
    {
        return new Vector2Serializable(vector2);
    }
}