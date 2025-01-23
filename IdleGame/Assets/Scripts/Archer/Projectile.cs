using Unity.Mathematics.Geometry;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject archer;
    public GameObject target;

    private float archerX;
    private float targetX;

    float speed = 10f;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;


    void Start()
    {
        archer = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        archerX = archer.transform.position.x;
        targetX = target.transform.position.x;

        dist = targetX - archerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(archer.transform.position.y, target.transform.position.y, (nextX - archerX) / dist);
        height = 2*(nextX - archerX) * (nextX - targetX) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z );

        if(transform.position == target.transform.position)
        {
            Destroy(gameObject);
        }
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }
}
