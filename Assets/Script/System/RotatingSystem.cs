using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct RotatingSystem : ISystem
{
    [BurstCompile]

    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, rotationspeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>().WithAll<CanRotate>())
        {
            transform.ValueRW = transform.ValueRO.RotateZ(rotationspeed.ValueRO.value * deltaTime);
        }
    }
}
