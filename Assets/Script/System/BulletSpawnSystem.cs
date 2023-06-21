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
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var isPressedSpace = Input.GetAxisRaw("Fire1");
        foreach (var transform1 in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Player>())
        {
            foreach (var (transform, bullet) in SystemAPI.Query<RefRW<LocalToWorld>, RefRW<Bullet>>())
            {
                if (isPressedSpace == 0)
                {
                    bullet.ValueRW.lastSpawnedTime = 0;
                }
                else
                {
                    if (bullet.ValueRO.lastSpawnedTime <= 0)
                    {
                        var entity = state.EntityManager.Instantiate(bullet.ValueRO.prefab);

                        foreach (var (transform2, bulletSpeed, ent) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BulletSpeed>>().WithEntityAccess())
                        {
                            if (!bulletSpeed.ValueRW.shootOut) // TO AVOID ALL BULLET RESET BACK TO THE GUN POSITION
                            {
                                transform2.ValueRW.Position = transform1.ValueRO.Position + new float3(0.8f, 0, 0);
                                bulletSpeed.ValueRW.shootOut = true;
                            }
                            ecb.SetComponent(ent, new PhysicsVelocity
                            {
                                Linear = new float3(0, 0, bulletSpeed.ValueRW.value)
                            });
                        }
                        bullet.ValueRW.lastSpawnedTime = bullet.ValueRO.spawnSpeed;
                    }
                    else
                    {
                        bullet.ValueRW.lastSpawnedTime -= SystemAPI.Time.DeltaTime;
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
