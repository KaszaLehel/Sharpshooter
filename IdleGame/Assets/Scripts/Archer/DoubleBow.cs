using UnityEngine;

public class DoubleBow : MonoBehaviour
{
    [Header("Archer Settings")]
    public GameObject arrow;
    public Transform shotPoint;
    public Transform[] targetPoints;
    //public float timeToTarget = 1f;
    public int damage = 0;
    //private int currentTargetindex = 0; 

    [SerializeField] private Animator animator;

    [Header("Timer Settings")]
    private float timer = 0f;
    private Vector2[] targetPositions = new Vector2[5];

#region Update
    void Update()
    {
        timer += Time.deltaTime;


        if (timer >= GameManager.Instance.shotTime)
        {
            if (GameManager.Instance.shootBoth)
            {
                //animator.SetTrigger("attack");
                ShootBothTargets();
            }
            else
            {
                //animator.SetTrigger("attack");
                Shoot();
            }
            timer = 0f;
        }
    }
#endregion



#region Shoot Metodus
    void Shoot()
    {
        if (targetPoints.Length == 0)
        {
            Debug.Log("No Target Point.");
            return;
        }

        int attempts = 0;

        while (targetPoints[GameManager.Instance.currentTargetindex] == null || !targetPoints[GameManager.Instance.currentTargetindex].gameObject.activeSelf) //activeHierarchy
        {
            GameManager.Instance.currentTargetindex = (GameManager.Instance.currentTargetindex + 1) % targetPoints.Length;

            attempts++;

            if (attempts >= targetPoints.Length)
            {
                return;
            }
        }

        Transform activeTarget = targetPoints[GameManager.Instance.currentTargetindex];
        if (activeTarget == null)
        {
            Debug.Log("No active targets available.");
            return;
        }

        UpdateTargetPositions(activeTarget);

        Vector2 targetToShot;
        if (Random.value <= 0.01f)
        {
            Vector2 missOffset = GetMissedOffset(activeTarget);
            targetToShot = (Vector2)shotPoint.position + missOffset;
            damage = 0;
        }
        else if (Random.value <= GameManager.Instance.hitChance / 100)
        {
            targetToShot = targetPositions[0];
            damage = 25;
        }
        else
        {
            int randomIndex = Random.Range(1, 5); // 1-től 4-ig generál
            targetToShot = targetPositions[randomIndex];
            if(randomIndex == 1 || randomIndex == 4)
            {
                damage = 5;
            }
            else
            {
                damage = 10;
            }
        }

        //ShootArrow(targetToShot, targetPoints[currentTargetindex], arrow); //---------------------
        ShootArrow(targetToShot, activeTarget, arrow);
    }

#endregion

#region Both Target Shoot
    void ShootBothTargets()
    {
        if (targetPoints.Length == 0)
        {
            Debug.Log("No Target Point.");
            return;
        }

        foreach (Transform target in targetPoints)
        {
            if (target != null && target.gameObject.activeSelf) //target.gameObject.activeInHierarchy
            {
                UpdateTargetPositionsForTarget(target);

                Vector2 targetToShot;
                if (Random.value <= 0.01f)
                {
                    Vector2 missOffset = GetMissedOffset(target); //targetPoints[currentTargetindex]
                    targetToShot = (Vector2)shotPoint.position + missOffset;
                    damage = 0;
                }
                else if (Random.value <= GameManager.Instance.hitChance / 100)
                {
                    targetToShot = targetPositions[0];
                    damage = 25;
                }
                else
                {
                    int randomIndex = Random.Range(1, 5); // 1-től 4-ig generál
                    if(randomIndex == 1 || randomIndex == 4)
                    {
                        damage = 5;
                    }
                    else
                    {
                        damage = 10;
                    }
                    targetToShot = targetPositions[randomIndex];
                }

                ShootArrow(targetToShot, target, arrow);
            }
        }
    }
#endregion


#region ShootArrow
    void ShootArrow(Vector2 targetToShot, Transform _target, GameObject arrowPrefab)
    {
        GameManager.Instance.SetFirstShot(true);
/*        
        GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        Vector2 startPosition = shotPoint.position;
        Vector2 direction = targetToShot - startPosition;

        float gravity =Physics2D.gravity.y * rb.gravityScale;

        //float randomHeight = Random.Range(1f, 3f);

        float vx = direction.x / timeToTarget;
        float vy = (direction.y / timeToTarget) - (0.5f * gravity * timeToTarget);

        rb.linearVelocity = new Vector2(vx, vy);

        Arrow arrowScript = newArrow.GetComponent<Arrow>();
        arrowScript.target = _target;
        arrowScript.arrowDamage = damage;
*/
        Vector2 direction = (targetToShot - (Vector2)shotPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
        GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, Quaternion.Euler(0, 0, angle));

        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        Vector2 startPosition = shotPoint.position;
        Vector2 targetPosition = targetToShot;

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        float randomHeight = Random.Range(1f, 2f);

        float maxHeight = Mathf.Max(startPosition.y, targetPosition.y) + randomHeight;

        float timeToApex = Mathf.Sqrt(2 * (maxHeight - startPosition.y) / gravity);

        float totalFlightTime = timeToApex + Mathf.Sqrt(2 * (maxHeight - targetPosition.y) / gravity);

        float vx = (targetPosition.x - startPosition.x) / totalFlightTime;

        float vy = gravity * timeToApex;

        rb.linearVelocity = new Vector2(vx, vy);

        Arrow arrowScript = newArrow.GetComponent<Arrow>();
        arrowScript.target = _target;
        arrowScript.arrowDamage = damage;
    }

#endregion

#region Missing Arrow
    Vector2 GetMissedOffset(Transform target)
    {
        Vector2 directionToTarget = (target.position - shotPoint.position).normalized;
        float missDistance = 1.5f; 
        return directionToTarget * missDistance;
    }
#endregion

#region Update Target
    void UpdateTargetPositions(Transform _currentTarget)
    {
        Transform currentTarget = _currentTarget;//targetPoints[currentTargetindex];
        if (currentTarget != null)
        {
            SetTargetPositions(currentTarget);
        }
        else
        {
            Debug.LogWarning("Current target is null during UpdateTargetPositions.");
        }
    }

    void UpdateTargetPositionsForTarget(Transform target)
    {
        if (target != null)
        {
            SetTargetPositions(target);
        }
        else
        {
            Debug.LogWarning("Target is null during UpdateTargetPositionsForTarget.");
        }
    }

    void SetTargetPositions(Transform target)
    {
        targetPositions[0] = target.position;
        targetPositions[1] = new Vector2(target.position.x, target.position.y + 1f);
        targetPositions[2] = new Vector2(target.position.x, target.position.y + 0.5f);
        targetPositions[3] = new Vector2(target.position.x, target.position.y - 0.5f);
        targetPositions[4] = new Vector2(target.position.x, target.position.y - 1f);
    }

#endregion

#region Gizmos
    void OnDrawGizmos()
    {
        if (shotPoint != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(shotPoint.position, 0.2f);
        }

        foreach (Transform target in targetPoints)
        {
            if (target != null)
            {
                Gizmos.color = Color.green;
                SetTargetPositions(target);

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
#endregion

}
