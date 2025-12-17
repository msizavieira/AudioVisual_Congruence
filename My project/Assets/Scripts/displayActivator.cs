using UnityEngine;

public class DisplayActivator : MonoBehaviour
{
    void Awake()
    {
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
    }
}
