using UnityEngine;

public class RedArrow : MonoBehaviour
{
    [Header("Archer Settings")]
    public GameObject greenArrow;
    public Transform shotPoint;
    public Transform[] targetPoints;
    //public float timeToTarget = 1f;
    public int damage = 0; 

    [Header("Timer Settings")]
    private float timer = 0f;
    private Vector2[] targetPositions = new Vector2[5];

#region Update
    void Update()
    {
        if(!GameManager.Instance.isRed) { return; }
        if(!GameManager.Instance.shootBoth) { return; }


        timer += Time.deltaTime;
        if (timer >= GameManager.Instance.redShotTime)
        {
            if (GameManager.Instance.shootBoth)
            {
                ShootBothTargets();
            }
            else
            {
                return;
                //Shoot();
            }
            timer = 0f;
        }
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
            if (target != null && target.gameObject.activeSelf)
            {
                UpdateTargetPositionsForTarget(target);

                Vector2 targetToShot;
                if (Random.value <= 0.01f)
                {
                    Vector2 missOffset = GetMissedOffset(target);
                    targetToShot = (Vector2)shotPoint.position + missOffset;
                    damage = 0;
                }
                else if (Random.value <= GameManager.Instance.redHitChance / 100)
                {
                    targetToShot = targetPositions[0];
                    damage = 100;
                }
                else
                {
                    int randomIndex = Random.Range(1, 5); // 1-től 4-ig generál
                    if(randomIndex == 1 || randomIndex == 4)
                    {
                        damage = 15;
                    }
                    else
                    {
                        damage = 30;
                    }
                    targetToShot = targetPositions[randomIndex];
                }

                ShootArrow(targetToShot, target, greenArrow);
            }
        }
    }
#endregion


#region ShootArrow
    void ShootArrow(Vector2 targetToShot, Transform _target, GameObject arrowPrefab)
    {
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

        GameObject newArrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
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
            Gizmos.color = Color.red;
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


