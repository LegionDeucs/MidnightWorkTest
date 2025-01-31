using UnityEngine;

[System.Serializable]
public class QuaternionSerializable
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionSerializable(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}