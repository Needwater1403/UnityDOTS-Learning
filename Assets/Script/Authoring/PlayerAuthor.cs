using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthor : MonoBehaviour
{
    public class PlayerBaker : Baker<PlayerAuthor>
    {
        public override void Bake(PlayerAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new Player
            {

            });
        }
    }
}

