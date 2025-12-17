using UnityEngine;

public class OpenEnding : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public GameObject toDisable;

    bool triggered = false;

    void Update()
    {
        if (triggered) return;

        float canopy = Vector3.Distance(player.position, target.position);

        if (canopy <= 2f)
        {
            toDisable.SetActive(false);
        }
    }


}
