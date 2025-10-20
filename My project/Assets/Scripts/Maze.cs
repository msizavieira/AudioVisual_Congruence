using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MazeGenerator : MonoBehaviour
{
    public Texture2D mazeImage;
    public GameObject wallPrefab;
    public float cellSize = 1f;
    public bool generateInEditor = false;

#if UNITY_EDITOR
    void Update()
    {
        // Only run in edit mode when checkbox is ticked
        if (!Application.isPlaying && generateInEditor)
        {
            generateInEditor = false; // reset checkbox
            ClearMaze();
            GenerateMaze();
        }
    }
#endif

    void ClearMaze()
    {
        // Remove previous maze objects (children)
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void GenerateMaze()
    {
        if (mazeImage == null || wallPrefab == null)
        {
            Debug.LogError("Assign mazeImage and wallPrefab!");
            return;
        }

        int width = mazeImage.width;
        int height = mazeImage.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = mazeImage.GetPixel(x, y);

                if (pixel.grayscale < 0.5f)
                {
                    Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                    GameObject wall = (GameObject)PrefabUtility.InstantiatePrefab(wallPrefab, transform);
                    wall.transform.localPosition = pos;
                }
            }
        }
    }
}
