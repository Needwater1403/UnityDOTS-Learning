using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

public partial struct DestroyEntitiesSystem : ISystem
{
    
    private int point;
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (deadStatus, entity) in SystemAPI.Query<RefRW<DeadStatus>>().WithEntityAccess())
        {   
            if (deadStatus.ValueRO.isDead)
            {
                ecb.DestroyEntity(entity);
                point++;
            }
        }
        foreach (var score in SystemAPI.Query<RefRW<ScoreBoard>>())
        {
            score.ValueRW.score = point/2;
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
        
    }
}