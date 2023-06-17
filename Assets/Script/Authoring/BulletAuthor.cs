using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletAuthor : MonoBehaviour
{
    public GameObject prefab;
    public float radius;
    public int num;
}
public class BulletBaker : Baker<BulletAuthor>
{
    public override void Bake(BulletAuthor authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(new Bullet
        {
            prefab = GetEntity(authoring.prefab,TransformUsageFlags.Dynamic),
            radius = authoring.radius,
            num = authoring.num,
        });
    }
}
