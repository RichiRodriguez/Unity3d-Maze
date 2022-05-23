using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{

	public IntVector2 size;

	public MazeCell cellPrefab;

	public float generationStepDelay;

	private MazeCell[,] cells;

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	public IntVector2 RandomCoordinates
	{
		get
		{
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell(IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate() {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0)
		{
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		MazeDirection direction = MazeDirections.RandomValue;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates))
		{
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
            }
			else {
				CreateWall(currentCell, neighbor, direction);
				activeCells.RemoveAt(currentIndex);
			}
		}
		else
		{
			CreateWall(currentCell, null, direction);
			activeCells.RemoveAt(currentIndex);
		}
	}

    private void CreateWall(MazeCell currentCell, MazeCell neighbor, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initalize(currentCell, neighbor, direction);
		if (neighbor != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initalize(neighbor, currentCell, direction.GetOpposite());
        }
    }

    private void CreatePassage(MazeCell currentCell, MazeCell neighbor, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initalize(currentCell, neighbor, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initalize(neighbor, currentCell, direction.GetOpposite());
    }

    private MazeCell CreateCell(IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}
}