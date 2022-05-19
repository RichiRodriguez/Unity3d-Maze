using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public MazeCell cellPrefab;
    private MazeCell[,] cells;
    public float generationStepDelay;
    public IntVector2 size;

    public IEnumerator Generate() 
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        IntVector2 coordinates = RandomCoordinates;
        //for (int x = 0; x < size.x; x++)
        //{
        //    for (int z = 0; z < size.z; z++)
        //    {
        //        yield return delay;
        //        CreateCell(new IntVector2(x, z));
        //    }
        //}
        while (ContainsCoordinate(coordinates))
        {
            yield return delay;
            CreateCell(coordinates);
            coordinates.z += 1;
        }
    }

    private void CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = $"Maze Cell {coordinates.x}, {coordinates.z}";
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - (size.x * 0.5f) + 0.5f, 0f, coordinates.z - (size.z * 0.5f) + 0.5f);
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinate(IntVector2 coordiante)
    {
        return coordiante.x >= 0 && coordiante.x < size.x && coordiante.z >= 0 && coordiante.z < size.z;
    }
}
