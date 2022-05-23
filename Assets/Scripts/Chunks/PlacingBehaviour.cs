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

    public ButtonBehaviour buttons;
    public BuyingLandBehaviour land;

    public BuildingObjects[] types;

    public TileBase construction;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 inputPos = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < buttons.buildings.Length; i++)
            {
                if (buttons.buildings[i] && !buttons.cantBuildDestroy && land.isOwned(inputPos))
                {
                    for (int j = 0; j < types[i].placeableTerrain.Length; j++)
                    {
                        TileBase inputTerrain = terrain.GetTile(objects.WorldToCell(inputPos));
                        TileBase inputObject = objects.GetTile(objects.WorldToCell(inputPos));
                        if (types[i].placeableTerrain[j] == inputTerrain && inputObject == null || (types[i].onObjects && types[i].placeableTerrain[j] == inputObject))
                        {
                            if (coins.coins >= types[i].price)
                            {
                                coins.coins -= types[i].price;
                                StartCoroutine(placeObjectTime(objects.WorldToCell(inputPos).x, objects.WorldToCell(inputPos).y, types[i].buildTime, types[i].type, types[i].coinsAmount, types[i].coinsTime, types[i]));
                            }
                        } 
                    }
                }
            }
        }
    }

    public IEnumerator placeObjectTime(int posX, int posY, float seconds, TileBase Tile, int coinsAmount, float coinsSeconds, BuildingObjects thisTile)
    {
        objects.SetTile(objects.WorldToCell(new Vector2(posX, posY)), construction);
        Slider buildSlider2 = Instantiate(buildSlider, new Vector2(posX + 0.5f, posY + 0.1f), Quaternion.identity, canvas.transform);
        buildSlider2.value = 0;
        buildSlider2.maxValue = seconds;
        float temporary = 0;
        while (true)
        {
            temporary += Time.deltaTime;
            buildSlider2.value += Time.deltaTime;
            if (temporary >= seconds)
            {
                objects.SetTile(objects.WorldToCell(new Vector2(posX,posY)), Tile); 
                StartCoroutine(coins.generateMoneyOverTime(coinsAmount, posX, posY, coinsSeconds, thisTile));
                Destroy(buildSlider2.gameObject);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
