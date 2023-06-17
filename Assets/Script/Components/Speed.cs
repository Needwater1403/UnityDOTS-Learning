using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System.ComponentModel;
using Unity.Mathematics;

public struct Speed : IComponentData
{
    public float value;
    public float3 dir;
}
