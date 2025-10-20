using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    public string participantID = "P001";
    public string condition = "Water_Congruent";
    private string filePath;
    private float startTime;

    void Start()
    {
        filePath = Application.persistentDataPath + $"/{participantID}_{condition}.csv";
        startTime = Time.time;
        File.WriteAllText(filePath, "Time,PosX,PosY,PosZ\n");
    }

    void Update()
    {
        string line = $"{Time.time - startTime},{transform.position.x},{transform.position.y},{transform.position.z}\n";
        File.AppendAllText(filePath, line);
    }
}
