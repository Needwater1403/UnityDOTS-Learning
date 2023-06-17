using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CubeAuthor : MonoBehaviour
{
    // Start is called before the first frame update
    public class CubeBaker : Baker<CubeAuthor>
    {
        public override void Bake(CubeAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new Cube
            {

            });
        }
    }
}
