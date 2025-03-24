using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElementsToHide;
    [SerializeField] private TextMeshProUGUI[] textElementsToHide;
    public float idleTimeThreshold = 5f;
    private float idleTimer = 0f;
    private Vector3 lastMousePosition;
    private bool uiVisible = true;
    private float fadeDuration = 1.0f;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if (IsMouseMoving())
        {
            idleTimer = 0f;
            if (!uiVisible)
            {
                StartCoroutine(FadeUIElements(0, 1, fadeDuration));
                uiVisible = true;
            }
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeThreshold && uiVisible)
            {
                StartCoroutine(FadeUIElements(1, 0, fadeDuration));
                uiVisible = false;
            }
        }
    }

#region Checking is Mouse Moving?    
    bool IsMouseMoving()
    {
        if(Input.mousePosition != lastMousePosition)
        {
            lastMousePosition = Input.mousePosition;
            //Debug.Log("True");
            return true;
        }
        return false;
    }
#endregion

#region IEnumerator for fadeOut
    IEnumerator FadeUIElements(float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time <= endTime)
        {
            float t = Mathf.Clamp01((Time.time - startTime) / duration);
            Color newColor = Color.Lerp(new Color(1f, 1f, 1f, startAlpha), new Color(1f, 1f, 1f, endAlpha), t);

            foreach (GameObject uiElement in uiElementsToHide)
            {
                Image image = uiElement.GetComponent<Image>();
                if (image != null)
                {
                    image.color = newColor;
                }
                else
                {
                    Debug.Log("Nincs elem vagy szin");
                }
            }

            foreach (TextMeshProUGUI textElement in textElementsToHide)
            {
                if (textElement != null)
                {
                    
                    textElement.color = newColor;
                }
                else
                {
                    Debug.Log("Nincs text vagy szin");
                }
            }

            yield return null;
        }


        foreach (GameObject uiElement in uiElementsToHide)
        {
            if(uiElement != null)
            {
                Image image = uiElement.GetComponent<Image>();

                if (image != null)
                {
                    Color finalColor = Color.white;
                    finalColor.a = endAlpha;
                    image.color = finalColor;
                }
            }
            
            
        }

        foreach (TextMeshProUGUI textElement in textElementsToHide)
        {
            if (textElement != null)
            {
                
                Color finalColor = Color.white;
                finalColor.a = endAlpha;
                textElement.color = finalColor;
            }
        }
    }
#endregion

}
