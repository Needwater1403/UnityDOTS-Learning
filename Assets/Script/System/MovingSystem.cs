using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Physics;
using Unity.Jobs;

public partial struct Moving : IJobEntity
{
    public void Execute(ref LocalTransform transform, ref Speed speed)
    {
        if (transform.Position.x <= 9.1 && transform.Position.x >= -20.1)
        {
            transform.Position += new float3(speed.value, 0, 0) * speed.dir;
        }
        else
        {
            speed.dir.x = -speed.dir.x;
            transform.Position += new float3(speed.value, 0, 0) * speed.dir;
        }
    }
}
public partial struct MovingSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // ALREADY TRIED TO USE ENTITYMANAGER.HASCOMPONENT<T>() BUT DID NOT WORK (ERROR)

        var job = new Moving { };
        JobHandle jobHandle = job.Schedule(state.Dependency);
        jobHandle.Complete();
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (transform, speed, cube, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Speed>, RefRO<Cube>>().WithEntityAccess())
        {
            if(speed.ValueRW.dir.x == 1)
            {
                ecb.RemoveComponent<CanRotate>(entity);
            }
            else
            {
                ecb.AddComponent<CanRotate>(entity);
            }
        }
        foreach (var (transform, speed, cap, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Speed>, RefRO<Capsule>>().WithEntityAccess())
        {
            if (speed.ValueRW.dir.x == 1)
            {
                ecb.RemoveComponent<CanRotate>(entity);
            }
            else
            {
                ecb.AddComponent<CanRotate>(entity);
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
