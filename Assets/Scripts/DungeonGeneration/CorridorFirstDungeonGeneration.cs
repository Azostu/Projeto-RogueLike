using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGeneration : SimpleRandomWalkGeneretor
{
    [SerializeField]
    private int corridorLenght = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private void Start()
    {
        GenerateDungeon();
    }


    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potencialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potencialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potencialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);

        }

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        enemySpawner.EnemySpawning(roomPositions);

        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count(); i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }

        return newCorridor;
    }


    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in deadEnds)
        {
            if (roomPositions.Contains(position) == false)
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, position);
                roomPositions.UnionWith(roomFloor);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neightbours = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neightbours++;
            }
            if (neightbours == 1)
                deadEnds.Add(position);
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potencialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potencialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potencialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potencialRoomPositions)
    {
        var currentPosition = startingPosition;
        potencialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorCount; i++)
        {
            var path = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLenght);
            corridors.Add(path);
            currentPosition = path[path.Count - 1];
            potencialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(path);
        }

        return corridors;
    }
}
