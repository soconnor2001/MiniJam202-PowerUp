using UnityEngine;
using System;

public class RandomPointInCircle : MonoBehaviour 
{
    public Transform Center;
    public Transform Parent;
    public float radius = 5;
    public int ObjectsToGenerate = 20;
    private void Start()
    {
        for (int i = 0; i < ObjectsToGenerate; i++)
        {
            GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObject.transform.parent = Parent;
            newObject.transform.localScale = new Vector3(.1f, .1f, .1f);
            newObject.transform.position = GetRandomPointInCircle(Center.position, radius);
        }

        
    }
    public static Vector3 GetRandomPointInCircle(Vector3 center, float radius)
    {
        double randRadius = radius * Math.Sqrt(UnityEngine.Random.value);
        double theta = UnityEngine.Random.value * 2 * Math.PI;
        double x = center.x + randRadius * Math.Sin(theta);
        double z = center.z + randRadius * Math.Cos(theta);

        return new Vector3((float)x, center.y, (float)z);
    }
}
