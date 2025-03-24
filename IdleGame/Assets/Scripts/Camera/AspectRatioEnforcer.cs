using UnityEngine;
using Unity.Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("Cinemachine/Extensions/Aspect Ratio Enforcer")]
public class AspectRatioEnforcer : CinemachineExtension
{
    public float targetAspect = 16f / 9f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {

        if (!Application.isPlaying)
            return;

        if (stage == CinemachineCore.Stage.Body)
        {
            
            float windowAspect = (float)Screen.width / Screen.height;
            
            float scaleFactor = windowAspect / targetAspect;

            Camera mainCamera = Camera.main;
            if (mainCamera == null)
                return;


            if (scaleFactor < 1.0f)
            {
                float inset = (1.0f - scaleFactor) / 2.0f;
                mainCamera.rect = new Rect(0, inset, 1.0f, scaleFactor);
            }
            else
            {
                float scaleWidth = 1.0f / scaleFactor;
                float inset = (1.0f - scaleWidth) / 2.0f;
                mainCamera.rect = new Rect(inset, 0, scaleWidth, 1.0f);
            }
        }
    }

    private void OnDisable()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.rect = new Rect(0, 0, 1, 1);
        }
    }

}
