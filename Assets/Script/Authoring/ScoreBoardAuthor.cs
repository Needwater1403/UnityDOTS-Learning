using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardAuthor : MonoBehaviour
{
    public int score;
}
public class ScoreBoardBaker : Baker<ScoreBoardAuthor>
{
    public override void Bake(ScoreBoardAuthor authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(new ScoreBoard
        {   
            score = authoring.score,
        });
    }
}
