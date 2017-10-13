using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    public static bool isJump = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isJump = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJump = false;
    }
    
}
