using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities;

public class SpeedAuthor : MonoBehaviour
{
    public float value;
    public float3 dir;
}
public class SpeedBaker : Baker<SpeedAuthor>
{
    public override void Bake(SpeedAuthor authoring)
    {   
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(new Speed
        {
            value = authoring.value,
            dir = authoring.dir
        });
    }
}
