using UnityEngine;

public class MazeCell : MonoBehaviour
{

	public IntVector2 coordinates;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	public MazeCellEdge GetEdge(MazeDirection direction) {
		return edges[(int)direction];
    }

	public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
    }
}