using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Maze mazePrefab;

    public int sizeX, sizeY;
    public MazeCell cellPrefab;
    private MazeCell[,] cells;

    private Maze mazeInstance;
    void Start()
    {
        BeginGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        //Debug.Log($"Creating new game: {mazeInstance.gameObject.name}");
    }

    private void RestartGame()
    {
        //Debug.Log($"Destroying {mazeInstance.gameObject.name}");
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}
