using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlacingBehaviour : MonoBehaviour
{
    public CoinBehaviour coins;

    public Canvas canvas;

    public Slider buildSlider;

    Tilemap objects => GameObject.Find("Grid").transform.Find("Objects").GetComponent<Tilemap>();
    Tilemap terrain => GameObject.Find("Grid").transform.Find("Floors").GetComponent<Tilemap>();
    Tilemap produce => GameObject.Find("Grid").transform.Find("Produce").GetComponent<Tilemap>();

    public ButtonBehaviour buttons;
    public BuyingLandBehaviour land;
    public ProductionBehaviour production;

    public BuildingObject[] types;

    public TileBase construction;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 inputPos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            TileBase inputTerrain = terrain.GetTile(objects.WorldToCell(inputPos));
            TileBase inputObject = objects.GetTile(objects.WorldToCell(inputPos));
            for (int i = 0; i < types.Length; i++)
            {
                for (int j = 0; j < buttons.produce.Length; j++)
                {
                    if (buttons.produce[j] && types[i].isProduction && !buttons.cantBuildDestroy)
                    {
                        for (int k = 0; k < production.produceAmounts.Length; k++)
                        {
                            if (j == k && types[i] == production.pObjects[k].buildOn)
                            {
                                if (inputObject == types[i].type && !produce.HasTile(produce.WorldToCell(inputPos)) && production.produceAmounts[j] >= production.pObjects[k].tilePrice)
                                {
                                    produce.SetTile(produce.WorldToCell(inputPos), production.pObjects[k].tileFind);
                                    production.produceAmounts[j] -= production.pObjects[k].tilePrice;
                                    StartCoroutine(production.Produce(objects.WorldToCell(inputPos).x, objects.WorldToCell(inputPos).y, types[i], production.pObjects[k]));
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < buttons.buildings.Length; i++)
            {
                if (buttons.buildings[i] && !buttons.cantBuildDestroy && land.isOwned(inputPos))
                {
                    for (int j = 0; j < types[i].placeableTerrain.Length; j++)
                    {
                        if (types[i].placeableTerrain[j] == inputTerrain && inputObject == null || (types[i].onObjects && types[i].placeableTerrain[j] == inputObject))
                        {
                            if (coins.coins >= types[i].price)
                            {
                                coins.coins -= types[i].price;
                                StartCoroutine(placeObjectTime(objects.WorldToCell(inputPos).x, objects.WorldToCell(inputPos).y, types[i]));
                            }
                        } 
                    }
                }
            }
        }
    }

    public IEnumerator placeObjectTime(int posX, int posY, BuildingObject thisTile)
    {
        objects.SetTile(objects.WorldToCell(new Vector2(posX, posY)), construction);
        Vector3Int inputPos = objects.WorldToCell(new Vector2(posX, posY));
        Slider buildSlider2 = Instantiate(buildSlider, new Vector2(posX + 0.5f, posY + 0.1f), Quaternion.identity, canvas.transform);
        buildSlider2.value = 0;
        buildSlider2.maxValue = thisTile.buildTime;
        float temporary = 0;
        while (true)
        {
            temporary += Time.deltaTime;
            buildSlider2.value += Time.deltaTime;
            if (temporary >= thisTile.buildTime)
            {
                objects.SetTile(objects.WorldToCell(new Vector2(posX,posY)), thisTile.type); 
                if (!thisTile.isProduction)
                {
                    StartCoroutine(coins.generateMoneyOverTime(posX, posY, thisTile));
                }
                for (int k = 0; k < production.produceAmounts.Length; k++)
                {
                    if (thisTile == production.pObjects[k].buildOn)
                    {
                        produce.SetTile(produce.WorldToCell(inputPos), production.pObjects[k].tileFind);
                        production.produceAmounts[k] -= production.pObjects[k].tilePrice;
                        StartCoroutine(production.Produce(objects.WorldToCell(inputPos).x, objects.WorldToCell(inputPos).y, thisTile, production.pObjects[k]));
                    }
                }
                Destroy(buildSlider2.gameObject);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
