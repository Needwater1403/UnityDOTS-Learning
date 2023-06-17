using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotationSpeedAuthor : MonoBehaviour
{
    public float value;
}
public class RotationSpeedBaker : Baker<RotationSpeedAuthor>
{
    public override void Bake(RotationSpeedAuthor authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(new RotationSpeed
        {
            value = authoring.value
        });
    }
}
