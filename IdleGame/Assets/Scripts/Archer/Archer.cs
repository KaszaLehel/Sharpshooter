using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform target;
    public Transform shootPoint;
    public float shootInterval = 2f;
    public float shootingSpeed = 5f;

    private float shootTimer;

    

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            ShootArrow();
            shootTimer = shootInterval;
        }

    }

    void ShootArrow()
    {
        if(target == null) { return; }
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        Vector2 direction = (target.position - shootPoint.position).normalized;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * shootingSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
