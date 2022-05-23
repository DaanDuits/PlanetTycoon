using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeleteBehaviour : MonoBehaviour
{
    Vector3 before;
    Vector3 after;

    public CoinBehaviour coins;

    Tilemap objects => GameObject.Find("Grid").transform.Find("Objects").GetComponent<Tilemap>();

    public BuildingObjects[] types;
    public BuyingLandBehaviour land;

    public ButtonBehaviour buttons;

    private void Update()
    {
        if (buttons.Deleting && !buttons.cantBuildDestroy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                before = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                if (objects.HasTile(objects.WorldToCell(before)) && land.isOwned(before))
                {
                    TileBase tile = objects.GetTile(objects.WorldToCell(before));
                    for (int i = 0; i < types.Length; i++)
                    {
                        if (types[i].type == tile && coins.coins >= types[i].neededValue)
                        {
                            coins.coins += types[i].value;
                            land.RemoveFromList(objects.GetTile(objects.WorldToCell(before)));
                            objects.SetTile(objects.WorldToCell(before), null);
                        }
                    }
                }
            }
        }
    }
}
