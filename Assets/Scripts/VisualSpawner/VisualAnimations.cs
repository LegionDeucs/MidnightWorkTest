using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class VisualAnimations
{
    public static Quaternion GetRotationAnimationFrame(float progression, RotationAnimationData itemAnimationData, bool isInLocalCoordinates, List<Quaternion> targetRotation)
    {
        Quaternion defaultRotation = isInLocalCoordinates ? itemAnimationData.localRotation : itemAnimationData.rotation;
        List<Quaternion> rotations = new List<Quaternion>(targetRotation);
        for (int i = 0; i < rotations.Count; i++)
            rotations[i] = rotations[i] * defaultRotation;

        rotations.Insert(0, defaultRotation);
        rotations.Add(defaultRotation);


        if (rotations == null || rotations.Count < 2)
            return defaultRotation;

        float totalDistance = 0f;
        List<float> cumulativeDistances = new List<float> { 0f };

        for (int i = 1; i < rotations.Count; i++)
        {
            float distance = Quaternion.Angle(rotations[i - 1], rotations[i]);
            totalDistance += distance;
            cumulativeDistances.Add(totalDistance);
        }
        float targetDistance = progression * totalDistance;

        int startIndex = 0;
        while (startIndex < cumulativeDistances.Count - 1 &&
               cumulativeDistances[startIndex + 1] <= targetDistance)
        {
            startIndex++;
        }

        if (startIndex >= rotations.Count - 1)
            return rotations[rotations.Count - 1];

        float segmentStart = cumulativeDistances[startIndex];
        float segmentEnd = cumulativeDistances[startIndex + 1];
        float localProgress = (targetDistance - segmentStart) / (segmentEnd - segmentStart);

        return Quaternion.Lerp(rotations[startIndex], rotations[startIndex + 1], localProgress);
    }

    public static Vector3 GetScaleAnimationFrame(float progression, Vector3 defaultScale, List<Vector3> targetScales)
    {
        List<Vector3> scales = new List<Vector3>(targetScales);

        scales.Insert(0, defaultScale);
        scales.Add(defaultScale);

        if (scales == null || scales.Count < 2)
            return defaultScale;

        float totalDistance = 0f;
        List<float> cumulativeDistances = new List<float> { 0f };

        for (int i = 1; i < scales.Count; i++)
        {
            float distance = Vector3.Distance(scales[i - 1], scales[i]);
            totalDistance += distance;
            cumulativeDistances.Add(totalDistance);
        }
        float targetDistance = progression * totalDistance;

        int startIndex = 0;
        while (startIndex < cumulativeDistances.Count - 1 &&
               cumulativeDistances[startIndex + 1] <= targetDistance)
        {
            startIndex++;
        }

        if (startIndex >= scales.Count - 1)
            return scales[scales.Count - 1];

        float segmentStart = cumulativeDistances[startIndex];
        float segmentEnd = cumulativeDistances[startIndex + 1];
        float localProgress = (targetDistance - segmentStart) / (segmentEnd - segmentStart);

        return Vector3.Lerp(scales[startIndex], scales[startIndex + 1], localProgress);
    }

    public static Vector3 GetPositionAnimationFrame(float progression, Vector3 defaultPosition, List<Vector3> targetPositions)
    {
        List<Vector3> positions = new(targetPositions);

        for (int i = 0; i < positions.Count; i++)
            positions[i] += defaultPosition;

        if (positions == null || positions.Count < 2)
            return defaultPosition;

        float totalDistance = 0f;
        List<float> cumulativeDistances = new() { 0f };

        for (int i = 1; i < positions.Count; i++)
        {
            float distance = Vector3.Distance(positions[i - 1], positions[i]);
            totalDistance += distance;
            cumulativeDistances.Add(totalDistance);
        }
        float targetDistance = progression * totalDistance;

        int startIndex = 0;
        while (startIndex < cumulativeDistances.Count - 1 &&
               cumulativeDistances[startIndex + 1] <= targetDistance)
        {
            startIndex++;
        }

        if (startIndex >= positions.Count - 1)
            return positions[positions.Count - 1];

        float segmentStart = cumulativeDistances[startIndex];
        float segmentEnd = cumulativeDistances[startIndex + 1];
        float localProgress = (targetDistance - segmentStart) / (segmentEnd - segmentStart);

        return Vector3.Lerp(positions[startIndex], positions[startIndex + 1], localProgress);
    }
}
