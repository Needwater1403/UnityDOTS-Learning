using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct DeadStatus : IComponentData
{
    public bool isDead;
}
