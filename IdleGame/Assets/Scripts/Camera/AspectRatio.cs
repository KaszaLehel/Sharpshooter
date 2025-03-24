using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Custom/Aspect Ratio Enforcer")]
public class AspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 9f; 

    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>(); 
        if (cameraComponent == null)
        {
            Debug.LogError("Camera component not found!");
        }
    }

    void Update()
    {
        if (!Application.isPlaying)
            return;

        
        float windowAspect = (float)Screen.width / Screen.height;

        
        float scaleFactor = windowAspect / targetAspect;

        
        if (scaleFactor < 1.0f)
        {
            
            float inset = (1.0f - scaleFactor) / 2.0f;
            cameraComponent.rect = new Rect(0, inset, 1.0f, scaleFactor);
        }
        else
        {
            
            float scaleWidth = 1.0f / scaleFactor;
            float inset = (1.0f - scaleWidth) / 2.0f;
            cameraComponent.rect = new Rect(inset, 0, scaleWidth, 1.0f);
        }
    }

    private void OnDisable()
    {
        if (cameraComponent != null)
        {
            cameraComponent.rect = new Rect(0, 0, 1, 1);
        }
    }
}
