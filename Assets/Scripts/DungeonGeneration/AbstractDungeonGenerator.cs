using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tileMapVisualizer = null;
    [SerializeField]
    protected Vector2Int startingPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tileMapVisualizer.Clear();
        RunProceduralGeneration();
    }
    public void GenerateDungeonWithoutEnemies()
    {
        tileMapVisualizer.Clear();
        RunProceduralGenerationWithoutEnemies();
    }

    protected abstract void RunProceduralGeneration();
    protected abstract void RunProceduralGenerationWithoutEnemies();

}
