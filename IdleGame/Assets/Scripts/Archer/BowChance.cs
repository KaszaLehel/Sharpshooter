using UnityEngine;

public class BowChance : MonoBehaviour
{
    [Header("Settings")]
    public GameObject arrow;
    //public float launchForce = 10f;
    public Transform shotPoint;
    public Transform[] targetPoints;  //targetPoint
    public float timeToTarget = 1f;

    //GameManager
    [Header("GameManager Settings")]
    public float shotTime = 2f;
    public float hitChance = 0.1f;
    public int currentTargetindex = 0;

    [Header("Timer Settings")]
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= shotTime)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        if(targetPoints.Length == 0)
        {
            Debug.Log("No Target Point.");
            return;
        }

        int attempts = 0;

        while (targetPoints[currentTargetindex] == null || !targetPoints[currentTargetindex].gameObject.activeInHierarchy)
        {
            currentTargetindex = (currentTargetindex + 1) % targetPoints.Length; 

            attempts++;

            if (attempts >= targetPoints.Length)
            {
                return;
            }
        }

        //UpdateTargetPositions();
/*
        Vector2 targetToShot = targetPositions[Random.Range(0, targetPositions.Length)];
        Debug.Log(targetToShot);

        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        Vector2 startPosition = shotPoint.position;
        //Vector2 targetPosition = targetToShot; 
        Vector2 direction = targetToShot - startPosition; //targetPosition - startPosition; 

        float gravity = Physics2D.gravity.y * rb.gravityScale;

        float vx = direction.x / timeToTarget;
        float vy = (direction.y / timeToTarget) - (0.5f * gravity * timeToTarget);

        rb.linearVelocity = new Vector2(vx, vy);
*/


        bool hitTarget = Random.value <= hitChance;
        Debug.Log(hitTarget);

        Vector2 targetToShot; //targetPoints[currentTargetindex];

        if(!hitTarget)
        {
            targetToShot = new Vector2(targetPoints[currentTargetindex].position.x, targetPoints[currentTargetindex].position.y + (Random.Range(0, 2) == 0 ? -1f : 1f));
        }
        else
        {
            targetToShot = targetPoints[currentTargetindex].position;

            //Debug.Log(targetPoints[currentTargetindex].position);
        }

        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();
        //newArrow.GetComponent<Rigidbody2D>().linearVelocity = shotPoint.right * launchForce;//transform.right * launchForce;
        //Debug.Log("ShotPoint Right Vector: " + shotPoint.right);

        Vector2 startPosition = shotPoint.position;
        Vector2 targetPosition = targetToShot; //targetPoints[currentTargetindex].position;
        Vector2 direction = targetPosition - startPosition; //.normalized;

        float gravity = Physics2D.gravity.y * rb.gravityScale;

        float vx = direction.x / timeToTarget;
        float vy = (direction.y / timeToTarget) - (0.5f * gravity * timeToTarget);

        rb.linearVelocity = new Vector2(vx, vy);

    }

    void OnDrawGizmos()
    {

        if (shotPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shotPoint.position, shotPoint.position + shotPoint.right * 2);
        }

         for (int i = 0; i < targetPoints.Length; i++)
        {
            if (targetPoints[i] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(targetPoints[i].position, 0.2f);
            }
        }
    }
}
