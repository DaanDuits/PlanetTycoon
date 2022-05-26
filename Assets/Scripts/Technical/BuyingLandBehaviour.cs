using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuyingLandBehaviour : MonoBehaviour
{
    public Tilemap tilemap => GameObject.Find("Grid").transform.Find("Land").GetComponent<Tilemap>();
    public Tilemap objects => GameObject.Find("Grid").transform.Find("Objects").GetComponent<Tilemap>();

    public ButtonBehaviour buttons;
    public CoinBehaviour coins;
    public TileBase tile;

    public BuildingObject[] objectTypes;

    public List<TileBase> ownedLand;
    public List<TileBase> ownedObjects;
    public bool isOwned(Vector2 inputPos)
    {
        if(tilemap.HasTile(tilemap.WorldToCell(inputPos)))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !buttons.cantBuildDestroy)
        {
            Vector2 inputPos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            if (buttons.specials[1])
            {
                if (!isOwned(inputPos) && coins.coins >= 50)
                {
                    coins.coins -= 50;
                    tilemap.SetTile(tilemap.WorldToCell(inputPos), tile);
                    ownedLand.Add(tilemap.GetTile(tilemap.WorldToCell(inputPos)));
                    if (objects.HasTile(objects.WorldToCell(inputPos)))
                    {
                        ownedObjects.Add(objects.GetTile(objects.WorldToCell(inputPos)));
                    }
                }
            }
            if(isOwned(inputPos))
            {
                for (int i = 0; i < buttons.buildings.Length; i++)
                {
                    if (!objectTypes[i].onObjects && buttons.buildings[i] && coins.coins >= objectTypes[i].price && !objects.HasTile(objects.WorldToCell(inputPos)) && tilemap.HasTile(tilemap.WorldToCell(inputPos)))
                    {
                        ownedObjects.Add(objectTypes[i].type);
                    }
                    if (objectTypes[i].onObjects && buttons.buildings[i] && coins.coins >= objectTypes[i].price && objects.HasTile(objects.WorldToCell(inputPos)) && tilemap.HasTile(tilemap.WorldToCell(inputPos)))
                    {
                        for (int j = 0;  j < objectTypes[i].placeableTerrain.Length;  j++)
                        {
                            if(objects.GetTile(objects.WorldToCell(inputPos)) == objectTypes[i].placeableTerrain[j])
                            {
                                ownedObjects.Remove(objects.GetTile(objects.WorldToCell(inputPos)));
                                ownedObjects.Add(objectTypes[i].type);
                            }
                        }
                    }
                }
            }
        }
    }
    public void RemoveFromList(TileBase tile)
    {
        ownedObjects.Remove(tile);
    }
}
