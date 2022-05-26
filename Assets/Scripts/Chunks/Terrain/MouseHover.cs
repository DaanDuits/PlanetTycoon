using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    private Color startcolor;

    public Renderer Renderer;

    private void Start()
    {
        Renderer = this.GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        startcolor = Renderer.material.color;
        Renderer.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        Renderer.material.color = startcolor;
    }
}
