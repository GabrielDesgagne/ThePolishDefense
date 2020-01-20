using UnityEngine;

public class Waypoints : MonoBehaviour {

    //the way is the same for all levels
    public static Transform[] points;

	void Awake () {
        PreInitialize();
	}

    void PreInitialize()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    public static void SetPoint(Transform transform)
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
