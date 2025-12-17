using UnityEngine;
using UnityEngine.UI;

public class MinimapDisplay : MonoBehaviour
{
    public MazePathRecorder recorder;
    public RawImage rawImage;

    void Start()
    {
        rawImage.texture = recorder.GetLiveTexture();
    }
}
