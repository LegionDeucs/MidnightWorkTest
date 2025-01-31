using UnityEngine;

public static class VectorHelper
{
    public static bool InRange(Vector3 firstVector, Vector3 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    public static float SqrDistance(Vector3 firstVector, Vector3 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    public static Vector3 Direction(Vector3 from, Vector3 to)
    {
        return to - from;
    }

    public static bool InRange(Vector2 firstVector, Vector2 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    public static float SqrDistance(Vector2 firstVector, Vector2 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    public static Vector2 Direction(Vector2 from, Vector2 to)
    {
        return to - from;
    }

    public static Vector2 RandomPositionInCircle2D(float maxRadius, float minRadius = 0f)
    {
        var direction = Random.insideUnitCircle.normalized;
        var distance = Random.Range(minRadius, maxRadius);

        return direction * distance;
    }

    public static Vector3 RandomPositionInCircle(float maxRadius, float minRadius = 0f, int y = 0)
    {
        var pos2D = RandomPositionInCircle2D(maxRadius, minRadius);
        return new Vector3(pos2D.x, y, pos2D.y);
    }

    public static Vector3 RandomPositionInCircle(Vector3 center, float maxRadius, float minRadius = 0f, int y = 0)
    {
        return center + RandomPositionInCircle(maxRadius, minRadius, y);
    }

    public static Vector3 RandomPositionInSphere(float maxRadius, float minRadius = 0f)
    {
        var direction = Random.insideUnitSphere.normalized;
        var distance = Random.Range(minRadius, maxRadius);

        return direction * distance;
    }

    public static Vector3 RandomPositionInSphere(Vector3 center, float maxRadius, float minRadius = 0f)
    {
        return center + RandomPositionInSphere(maxRadius, minRadius);
    }
}