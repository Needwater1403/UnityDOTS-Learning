using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEditor.Search;
using Unity.Physics;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct BulletSpawnSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var world = state.World.Unmanaged;
        //var bulletSpawn = SystemAPI.GetSingleton<Bullet>();

        if (Input.GetKeyDown("space"))
        {
            
            foreach (var (transform1, player) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Player>>())
            {
                foreach (var (transform, bullet) in SystemAPI.Query<RefRW<LocalToWorld>, RefRO<Bullet>>())
                {
                    //var bulletEntities = CollectionHelper.CreateNativeArray<Entity, RewindableAllocator>(bullet.ValueRO.num, ref world.UpdateAllocator);
                    //var entity = ecb.Instantiate(bullet.ValueRO.prefab);
                    var entity = state.EntityManager.Instantiate(bullet.ValueRO.prefab);
                    state.Dependency.Complete();
                    foreach (var (transform2, bulletSpeed, ent) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletSpeed>>().WithEntityAccess())
                    {   
                        if(!bulletSpeed.ValueRW.shootOut) // TO AVOID ALL BULLET RESET BACK TO THE GUN POSITION
                        {
                            transform2.ValueRW.Position = new float3(0.8f, 0, 0) + transform1.ValueRW.Position;
                            bulletSpeed.ValueRW.shootOut = true;
                        }
                        ecb.SetComponent(entity, new PhysicsVelocity
                        {
                            Linear = new float3(0, 0, bulletSpeed.ValueRW.value)
                        });
                    }
                }
            }
        }
        foreach (var (transform2, bulletSpeed, deadStatus) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletSpeed>, RefRW<DeadStatus>> ())
        {
            if (transform2.ValueRW.Position.z > 100)
            {
                deadStatus.ValueRW.isDead = true;
            }
        }
        ecb.Playback(state.EntityManager);
    }
}


//var entity = ecb.Instantiate(bulletSpawn.prefab);
//state.Dependency.Complete();
//foreach (var (transform, bullet) in SystemAPI.Query<RefRW<LocalToWorld>, RefRO<Bullet>>())
//{
//    ecb.SetComponent(entity, new LocalTransform
//    {
//        Position = transform.ValueRW.Position,
//    });

//}
//ecb.SetComponent(entity, new PhysicsVelocity
//{
//    Linear = new float3(0, 0, 10)
//});