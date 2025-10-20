using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public bool isCongruent = true;
    public LandmarkAudioManager.SoundType soundType;

    void Start()
    {
        LandmarkAudioManager[] landmarks = FindObjectsOfType<LandmarkAudioManager>();
        foreach (var landmark in landmarks)
        {
            landmark.isCongruent = isCongruent;
            landmark.soundType = soundType;
        }
    }
}
