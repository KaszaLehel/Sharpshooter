using UnityEngine;

public class PopupText : MonoBehaviour
{
    public float DestroyTime = 2f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public float MinY = -1f; 
    public float MaxY = 1f; 
    public float MinX = -2f; 
    public float MaxX = -0.5f; 

    //public float CircleRadius = 5f;
    //public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    void Start()
    {
        Destroy(gameObject, DestroyTime);

        float randomY = Random.Range(MinY, MaxY);
        float randomX = Random.Range(MinX, MaxX);
        transform.localPosition += Offset + new Vector3(randomX, randomY, 0);

/*
        transform.localPosition += Offset;

        Vector2 randomCirclePoint = Random.insideUnitCircle * CircleRadius;
        transform.localPosition += new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
*/
        /*transform.localPosition += new Vector3(Random.Range(RandomizeIntensity.x, RandomizeIntensity.x),
                                                Random.Range(RandomizeIntensity.y, RandomizeIntensity.y),
                                                Random.Range(RandomizeIntensity.z, RandomizeIntensity.z));*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 minPosition = transform.position + Offset + new Vector3(MinX, MinY, 0);
        Vector3 maxPosition = transform.position + Offset + new Vector3(MaxX, MaxY, 0);

        Gizmos.DrawLine(minPosition, new Vector3(minPosition.x, maxPosition.y, minPosition.z));
        Gizmos.DrawLine(minPosition, new Vector3(maxPosition.x, minPosition.y, minPosition.z));
        Gizmos.DrawLine(maxPosition, new Vector3(minPosition.x, maxPosition.y, maxPosition.z));
        Gizmos.DrawLine(maxPosition, new Vector3(maxPosition.x, minPosition.y, maxPosition.z));
    }

}
