using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class CanRotateAuthor : MonoBehaviour
{
    // Start is called before the first frame update
    public class CanRotateBaker : Baker<CanRotateAuthor>
    {
        public override void Bake(CanRotateAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new CanRotate
            {

            });
        }
    }
}
