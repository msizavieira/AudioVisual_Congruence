using UnityEngine;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class DebuggingScript : MonoBehaviour
{
    public Transform player;
    public Transform FirstCheckPoint;
    public Transform LastCheckPoint;
    public Transform endingPos;
    public MazePathRecorder mzp;

    bool triggered = false;

    Stopwatch sw = new Stopwatch();
    string firstCP = "-1";
    string lastCP = "-1";

    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name; //To get the filename congruent or incongruent
        sw.Start();
    }

    void Update()
    {
        if (triggered) return;

        FirstCheckPointTime();
        LastCheckPointTime();
        CheckEnd();

    }

    float HorizontalDistance(Transform a, Transform b)
    {
        Vector2 p1 = new Vector2(a.position.x, a.position.z);
        Vector2 p2 = new Vector2(b.position.x, b.position.z);
        return Vector2.Distance(p1, p2);
    }


    void CheckEnd()
    {
        if (lastCP == "-1") return;
        if (firstCP == "-1") return; // if checkpoints weren't triggered
        
        float d = HorizontalDistance(player, endingPos);

        if (d <= 0.5f)
        {
            triggered = true;
            sw.Stop();
            mzp.SavePNG();
            WriteTimeFile();
        }
        
    }

    void FirstCheckPointTime()
    {
        if (firstCP != "-1") return;

        float d = HorizontalDistance(player, FirstCheckPoint);

        if (d <= 2f)
        {
            firstCP = sw.Elapsed.ToString(@"hh\:mm\:ss\.fff");
            UnityEngine.Debug.Log("FirstCheckpoint= " + lastCP);
        }
    }

    void LastCheckPointTime()
    {
        if (firstCP == "-1") return; // if firstCP wasn't triggered
        if (lastCP != "-1") return;

        float d = HorizontalDistance(player, LastCheckPoint);

        if (d <= 2f)
        {
            lastCP = sw.Elapsed.ToString(@"hh\:mm\:ss\.fff");
            UnityEngine.Debug.Log("LastCheckpoint= " + lastCP);
        }     
    }

    void WriteTimeFile()
    {
        string dir = Application.persistentDataPath + "/MazeResults";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        
        int index = 1;
        string path;

        do
        {
            path = Path.Combine(dir, sceneName + "_Time_" + index + ".txt");
            index++;
        }
        while (File.Exists(path));

        using (StreamWriter w = new StreamWriter(path))
        {
            w.WriteLine("FirstCheckpoint= " + firstCP);
            w.WriteLine("LastCheckpoint= " + lastCP);
            w.WriteLine("End= " + sw.Elapsed.ToString(@"hh\:mm\:ss\.fff"));
        }
    
        UnityEngine.Debug.Log("Saved path file: " + path);

    }
}
