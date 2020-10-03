using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] waypoints;

    private void Awake()
    {
        print(waypoints.Length);
        waypoints = new Transform[waypoints.Length];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }


}
