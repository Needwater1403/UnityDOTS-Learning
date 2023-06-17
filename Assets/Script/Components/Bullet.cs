using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Bullet : IComponentData
{
    public Entity prefab;
    public float radius;
    public int num;
}
