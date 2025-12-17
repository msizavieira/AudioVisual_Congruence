using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MazePathRecorder : MonoBehaviour
{
    public Transform player;
    public Texture2D mazeImage;   // assign MazeMap.png
    public float WorldWidth = 55f; // width of maze in world units
    public float WorldHeight = 40f; // width of maze in world units

    private Texture2D drawTex;
    private int texWidth;
    private int texHeight;

    string sceneName;

    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        texWidth  = mazeImage.width;
        texHeight = mazeImage.height;

        drawTex = new Texture2D(
            texWidth,
            texHeight,
            TextureFormat.RGBA32,
            false
        );

        drawTex.SetPixels(mazeImage.GetPixels());
        drawTex.Apply();
    }

    void Update()
    {
        DrawPlayerPosition();
    }

    public Texture2D GetLiveTexture()
    {
        return drawTex;
    }

    void DrawPlayerPosition()
    {
        Vector3 p = player.position;

        // Normalize world position into 0–1 range
        float nx = p.x / WorldWidth;
        float ny = p.z / WorldHeight;

        // Convert to texture space
        int px = Mathf.Clamp(Mathf.RoundToInt(nx * (texWidth - 1)), 0, texWidth - 1);
        int py = Mathf.Clamp(Mathf.RoundToInt(ny * (texHeight - 1)), 0, texHeight - 1);

        int radius = 3;
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int tx = px + x;
                    int ty = py + y;

                    if (tx >= 0 && tx < texWidth && ty >= 0 && ty < texHeight)
                        drawTex.SetPixel(tx, ty, Color.blue);
                }
            }
        }

        drawTex.Apply();
    }

    string GetNextFilePath()
    {
        string dir = Application.persistentDataPath + "/MazeResults";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        int index = 1;
        string path;

        do
        {
            path = Path.Combine(dir, sceneName + "_MazePath_" + index + ".png");
            index++;
        }
        while (File.Exists(path));

        return path;
    }


    public void SavePNG()
    {
        string path = GetNextFilePath();

        byte[] bytes = drawTex.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

        Debug.Log("Saved path file: " + path);
    }

    
}
