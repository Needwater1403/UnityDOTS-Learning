using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletSpeedAuthor : MonoBehaviour
{
    public float value;
    public bool shootOut;
    public class BulletSpeedBaker : Baker<BulletSpeedAuthor>
    {
        public override void Bake(BulletSpeedAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new BulletSpeed
            {
                value = authoring.value,
                shootOut = authoring.shootOut
            });
        }
    }
}
