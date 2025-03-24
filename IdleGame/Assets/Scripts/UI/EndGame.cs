using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElementsToHide;
    [SerializeField] private TextMeshProUGUI[] textElementsToHide;
    [SerializeField] private GameObject uiEndToPopUp;
    public float fadeDuration = 0.5f;
    public float waitBeforeFade = 10f;

    private bool hasStartedFade = false;

    void Start()
    {
        uiEndToPopUp.SetActive(false);
    }

    void Update()
    {
        if(GameManager.Instance.isGameFinished && !hasStartedFade)
        {
            hasStartedFade = true;
            StartCoroutine(StartFadeProcess());
        }
    }


    IEnumerator StartFadeProcess()
    {
        yield return new WaitForSeconds(waitBeforeFade);
        yield return StartCoroutine(FadeUIElements(1f, 0f, fadeDuration));

        foreach(GameObject uiElemnt in uiElementsToHide)
        {
            if(uiElemnt != null)
            {
                uiElemnt.SetActive(false);
            }
        }
        foreach(TextMeshProUGUI textElement in textElementsToHide)
        {
            if(textElement != null)
            {
                textElement.gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(1);
        uiEndToPopUp.SetActive(true);
    }


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
