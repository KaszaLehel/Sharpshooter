using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float targetAspect = 16f / 9f;

    void Start ()
    {
        AdjustCamraSize();
    }

    void AdjustCamraSize()
    {
        float windowAspect = (float) Screen.width / Screen.height;
        Debug.Log("Screen Size: " + windowAspect);

        float scaleFactor = windowAspect / targetAspect;
        Debug.Log(scaleFactor);

        Camera camera = GetComponent<Camera>();
        Debug.Log(camera.orthographicSize);

        if(scaleFactor < 1.0f)
        {
            camera.orthographicSize = camera.orthographicSize  / scaleFactor;
            Debug.Log(camera.orthographicSize);
        }
    }
    
}
