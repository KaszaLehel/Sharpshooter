using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyStartInstruction : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Destroy(gameObject);
        }
    }
}
