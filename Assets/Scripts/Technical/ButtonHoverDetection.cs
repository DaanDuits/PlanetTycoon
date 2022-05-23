using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonBehaviour buttons;
    public void OnPointerEnter(PointerEventData eventData)
    {  
        buttons.cantBuildDestroy = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttons.cantBuildDestroy = false;
    }
}
