using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Archer Settings")]
    public GameObject arrow;
    public Transform shotPoint;
    public Transform[] targetPoints; 
    public float timeToTarget = 1f;
    public int currentTargetindex = 0;
    //public bool shootBoth = false;

    [Header("Timer Settings")]
    private float timer = 0f;
    private Vector2[] targetPositions = new Vector2[5];

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= GameManager.Instance.shotTime)
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

        UpdateTargetPositions();

        Vector2 targetToShot;
        if(Random.value <= 0.01f)
        {
            Vector2 missOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            targetToShot = (Vector2)shotPoint.position + missOffset;
            Debug.Log("Missed the target!");
        }
        else if(Random.value <= GameManager.Instance.hitChance / 100)
        {
            targetToShot = targetPositions[0];
            Debug.Log("0");
        }
        else
        {
            int randomIndex = Random.Range(1, 5); //1-től 4-ig generál
            targetToShot = targetPositions[randomIndex];
            Debug.Log(randomIndex);
        }

        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        Vector2 startPosition = shotPoint.position;
        Vector2 direction = targetToShot - startPosition; 

        float gravity = Physics2D.gravity.y * rb.gravityScale;

        float vx = direction.x / timeToTarget;
        float vy = (direction.y / timeToTarget) - (0.5f * gravity * timeToTarget);

        rb.linearVelocity = new Vector2(vx, vy);
    }

    void UpdateTargetPositions()
    {
        Transform currentTarget = targetPoints[currentTargetindex];
        if (currentTarget != null)
        {
            targetPositions[0] = currentTarget.position; 
            targetPositions[1] = new Vector2(currentTarget.position.x, currentTarget.position.y + 1f);  
            targetPositions[2] = new Vector2(currentTarget.position.x, currentTarget.position.y + 0.5f);  
            targetPositions[3] = new Vector2(currentTarget.position.x, currentTarget.position.y - 0.5f);  
            targetPositions[4] = new Vector2(currentTarget.position.x, currentTarget.position.y - 1f); 
        }
        else
        {
            Debug.LogWarning("Current target is null during UpdateTargetPositions.");
        }
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

            targetPositions[0] = targetPoints[i].position; 
            targetPositions[1] = new Vector2(targetPoints[i].position.x, targetPoints[i].position.y + 1f);  
            targetPositions[2] = new Vector2(targetPoints[i].position.x, targetPoints[i].position.y + 0.5f);
            targetPositions[3] = new Vector2(targetPoints[i].position.x, targetPoints[i].position.y - 0.5f);  
            targetPositions[4] = new Vector2(targetPoints[i].position.x, targetPoints[i].position.y - 1f);  

            
            Gizmos.DrawLine(targetPositions[1], targetPositions[0]);  
            Gizmos.DrawLine(targetPositions[0], targetPositions[4]);  
            Gizmos.DrawLine(targetPositions[1], targetPositions[2]);  
            Gizmos.DrawLine(targetPositions[0], targetPositions[3]);  

            
            foreach (Vector2 pos in targetPositions)
            {
                Gizmos.DrawWireSphere(pos, 0.1f); 
            }
            }
        }
    }
}
