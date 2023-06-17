using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.VisualScripting;

public partial struct CollisionEventDestroySystem : ISystem
{   
    struct ComponentDataHandles
    {
        public ComponentLookup<DeadStatus> deadStatus;
        public ComponentLookup<Damage> dmg;
        public ComponentLookup<HP> hp;
        public ComponentDataHandles(ref SystemState systemState)
        {
            deadStatus = systemState.GetComponentLookup<DeadStatus>(false); //SET READONLY TO FALSE ? (STILL LEARNING)
            dmg = systemState.GetComponentLookup<Damage>(false);
            hp = systemState.GetComponentLookup<HP>(false);
        }
        public void Update(ref SystemState systemState)
        {
            deadStatus.Update(ref systemState);
            dmg.Update(ref systemState);
            hp.Update(ref systemState);
        }
    }
    ComponentDataHandles data;  
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadWrite<DeadStatus>()));
        data = new ComponentDataHandles(ref state);
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        data.Update(ref state);
        state.Dependency = new CollisionEventDestroyJob
        {
            deadStatus = data.deadStatus,
            dmg = data.dmg,
            hp = data.hp
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        state.Dependency.Complete();
        foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Speed>>())
        {
            foreach (var (transform1, bulletSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletSpeed>>())
            {
                
            }
        }
    }
}


public struct CollisionEventDestroyJob : ICollisionEventsJobBase
{
    public ComponentLookup<DeadStatus> deadStatus;
    public ComponentLookup<Damage> dmg;
    public ComponentLookup<HP> hp;
    public void Execute(CollisionEvent collisionEvent)
    {
        // DO SOMETHING HERE
        Entity entB = collisionEvent.EntityB;
        Entity entA = collisionEvent.EntityA;
        
        var entAInflictDMG = dmg[entA];
        var entALoseHP = hp[entA];
        var entBInflictDMG = dmg[entB];
        var entBLoseHP = hp[entB];
        entBLoseHP.hp -= entAInflictDMG.dmg;
        entALoseHP.hp -= entBInflictDMG.dmg;
        
        if (entALoseHP.hp <= 0)
        {
            var DestroyComponent = deadStatus[entA];
            DestroyComponent.isDead = true;
            deadStatus[entA] = DestroyComponent;
        }
        if (entBLoseHP.hp <= 0)
        {
            var DestroyComponent = deadStatus[entB];
            DestroyComponent.isDead = true;
            deadStatus[entB] = DestroyComponent;
        }
    }
}