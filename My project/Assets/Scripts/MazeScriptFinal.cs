using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class MazeGenerator2 : MonoBehaviour
{
    [SerializeField] private MazeCell _mazeCellPrefab;
    [SerializeField] private float _cellSize = 2f;

    [SerializeField] private int _mazeWidth;

    [SerializeField] private int _mazeDepth;
    [SerializeField] private Transform _mazeParent;

    private MazeCell[,] _mazeGrid;

    public void GenerateInEditor()
    {
        ClearExisting();
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        Vector3 prefabOffset = _mazeCellPrefab.transform.localPosition;

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                Vector3 pos = new Vector3(x * _cellSize, _mazeCellPrefab.transform.position.y, z * _cellSize);

                var newObject = _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, pos, Quaternion.identity, _mazeParent);

                newObject.name = "Cell " + x + "," + z;
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
    }

    public void ClearExisting()
    {
        for (int i = _mazeParent.childCount - 1; i >= 0; i--)
            DestroyImmediate(_mazeParent.GetChild(i).gameObject);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.RoundToInt(currentCell.transform.localPosition.x / _cellSize);
        int z = Mathf.RoundToInt(currentCell.transform.localPosition.z / _cellSize);
        
        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        Vector3 prev = previousCell.transform.localPosition;
        Vector3 curr = currentCell.transform.localPosition;

        if (prev.x < curr.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (prev.x > curr.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (prev.z < curr.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (prev.z > curr.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(MazeGenerator2))]
public class MazeGeneratorEditor2 : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Maze in Editor"))
        {
            ((MazeGenerator2)target).GenerateInEditor();
        }

        if (GUILayout.Button("Clear Maze"))
        {
            ((MazeGenerator2)target).ClearExisting();
        }
    }
}
#endif
