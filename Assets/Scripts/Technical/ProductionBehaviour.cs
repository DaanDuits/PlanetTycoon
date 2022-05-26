using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ProductionBehaviour : MonoBehaviour
{
    public Canvas canvas;

    public Slider Slider;

    public int[] produceAmounts;
    public ProduceObject[] pObjects;
    public GameObject[] counts;
    public Text[] texts;

    public TileBase construction;

    Tilemap objects => GameObject.Find("Grid").transform.Find("Produce").GetComponent<Tilemap>();
    Tilemap buildings => GameObject.Find("Grid").transform.Find("Objects").GetComponent<Tilemap>();

    BuyingLandBehaviour land => GameObject.Find("Controllers").transform.Find("LandController").GetComponent<BuyingLandBehaviour>();


    private void Update()
    {
        for (int i = 0; i < counts.Length; i++)
        {
            if(produceAmounts[i] > 0)
            {
                counts[i].SetActive(true);
            }
            texts[i].text = produceAmounts[i].ToString();
        }
    }

    public IEnumerator Produce(int posX, int posY, BuildingObject type, ProduceObject produce)
    {
        GameObject newProduce = Instantiate(produce.type, new Vector2(posX + 0.5f, posY + 0.5f), Quaternion.identity);
        Vector3Int CellPos = objects.WorldToCell(new Vector3(posX, posY, 0));
        Slider Slider2 = Instantiate(Slider, new Vector2(posX + 0.5f, posY + 0.9f), Quaternion.identity, canvas.transform);
        Slider2.value = 0;
        Slider2.maxValue = produce.time;
        float temporary = 0;
        while (true)
        {
            temporary += Time.deltaTime;
            Slider2.value += Time.deltaTime;
            if (!buildings.HasTile(CellPos) || buildings.GetTile(CellPos) == construction)
            {
                produceAmounts[produce.i] += produce.tilePrice;
                Destroy(newProduce);
                objects.SetTile(CellPos, null);
                Destroy(Slider2.gameObject);
                yield break;
            }
            if (temporary >= produce.time)
            {
                produceAmounts[produce.i] += produce.amount;

                for (int j = 0; j < land.objectTypes.Length; j++)
                {
                    for (int k = 0; k < land.ownedObjects.Count; k++)
                    {
                        if (land.objectTypes[j].boostingTile != null && buildings.HasTile(CellPos))
                        {
                            if (land.objectTypes[j].type == land.ownedObjects[k] && buildings.GetTile(CellPos) == land.objectTypes[j].boostingTile.type)
                            {
                                produceAmounts[produce.i] += type.boost;
                            }
                        }
                    }
                }
                temporary = 0;
                Slider2.value = 0;

                if (produceAmounts[produce.i] > 0)
                {
                    produceAmounts[produce.i] -= produce.tilePrice;
                }

                else
                {
                    Destroy(newProduce);
                    objects.SetTile(CellPos, null);
                    Destroy(Slider2.gameObject);
                    yield break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
