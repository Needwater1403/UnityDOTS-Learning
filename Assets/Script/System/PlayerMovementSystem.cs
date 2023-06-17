using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {   
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical"); 
        float deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, player) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRW<Player>>())
        {
            transform.ValueRW.Linear = new Vector3(dirX * 5, dirZ * 5, 0); //DIRZ IS NOT EXACTLY DIRZ BECAUSE THIS PROTOTYPE IS TEMPORARY A 2D GAME
        }
        

    }
}
