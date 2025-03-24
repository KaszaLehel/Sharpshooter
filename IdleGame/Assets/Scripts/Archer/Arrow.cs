using System.Collections;
using TMPro;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   Rigidbody2D rb;
   bool hasHit = false;
   SpriteRenderer spriteRenderer;

   public Transform target;
   public GameObject PopupTextPrefab;
   public int arrowDamage;
   public bool isTargetActive = true;

   void Start()
   {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
   }

   void Update()
   {
        if (target != null && !target.gameObject.activeSelf)
        {
            isTargetActive = false;
            arrowDamage = 0;  
        }

        if (!hasHit)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Target") && collision.transform == target)
        {
            //Debug.Log(target);
            hasHit = true;
            rb.bodyType = RigidbodyType2D.Static;

            Target targetScript = collision.GetComponent<Target>();
            if (targetScript != null)
            {
                //Debug.Log(target.gameObject);
                targetScript.DamageTarget(arrowDamage);
            }

            if(PopupTextPrefab)
            {
                ShowPopupText();
            }



            StartCoroutine(FadeOut(3f));
        }

    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        if(collision.gameObject.CompareTag("Ground"))
        {
            hasHit = true;
            rb.bodyType = RigidbodyType2D.Static;
            
            StartCoroutine(FadeOut(1f));
        }
    } 

    IEnumerator FadeOut(float _fadeSpeed)
    {
        float targetAlpha = 0f; 
        Color startColor = spriteRenderer.color;
        //float startAlpha = startColor.a;

        while (startColor.a > targetAlpha)
        {
            startColor.a -= _fadeSpeed * Time.deltaTime;
            spriteRenderer.color = startColor;
            yield return null; 
        }

        Destroy(gameObject);
    } 

    private void ShowPopupText()
    {
        var go = Instantiate(PopupTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = arrowDamage.ToString();
    }  
}
