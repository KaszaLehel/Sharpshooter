using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySecondTargetPanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                Destroy(gameObject);
            }
        }
    }
}
