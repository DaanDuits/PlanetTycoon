using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonBehaviour buttons => GameObject.Find("Controllers").transform.Find("ButtonController").GetComponent<ButtonBehaviour>();
    public void OnPointerEnter(PointerEventData eventData)
    {  
        buttons.cantBuildDestroy = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttons.cantBuildDestroy = false;
    }
}
