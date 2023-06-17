using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CapsuleAuthor : MonoBehaviour
{
    
    public class CapsualBaker : Baker<CapsuleAuthor>
    {
        public override void Bake(CapsuleAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new Capsule
            {
                
            });
        }
    }
}
