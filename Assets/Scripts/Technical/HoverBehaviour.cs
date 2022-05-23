using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverBehaviour : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 a = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        float x= Mathf.Floor(a.x) + 0.5f;
        float y = Mathf.Floor(a.y) + 0.5f;
        transform.position = new Vector2(x,y);
    }
}
