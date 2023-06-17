using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DeadStatusAuthor : MonoBehaviour
{
    public bool isDead;
    public class DeadStatusBaker : Baker<DeadStatusAuthor>
    {
        public override void Bake(DeadStatusAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new DeadStatus
            {
                isDead = authoring.isDead
            });
        }
    }
}
