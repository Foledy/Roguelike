using Unity.Mathematics;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float _sensitive;

    public void Rotate(float2 delta)
    {
        transform.Rotate(new Vector3(delta.x, delta.y, 0));
    }
}