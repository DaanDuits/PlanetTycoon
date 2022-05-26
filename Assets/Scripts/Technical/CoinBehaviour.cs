using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    public BuyingLandBehaviour land;

    public Canvas canvas;
    public TileBase construction;

    public Slider coinSlider;

    public Text coinsText;
    public int coins = 0;
    public int startAmount;

    public float taxTime;

    public BuildingObject[] buildingTypes;
    Tilemap objects => GameObject.Find("Grid").transform.Find("Objects").GetComponent<Tilemap>();

    private void Start()
    {
        coins = startAmount;
        StartCoroutine(Taxes(taxTime));
    }
    private void Update()
    {
        coinsText.text = coins.ToString();
    }

    public IEnumerator generateMoneyOverTime(int posX, int posY, BuildingObject thisTile)
    {
        Vector3Int CellPos = objects.WorldToCell(new Vector3(posX, posY, 0));
        if(thisTile.coinsTime != 0)
        {
            Slider coinSlider2 = Instantiate(coinSlider, new Vector2(posX + 0.5f, posY + 0.9f), Quaternion.identity, canvas.transform);
            coinSlider2.value = 0;
            coinSlider2.maxValue = thisTile.coinsTime;
            float temporary = 0;
            while (true)
            {
                temporary += Time.deltaTime;
                coinSlider2.value += Time.deltaTime;
                if (temporary >= thisTile.coinsTime)
                {
                    coins += thisTile.coinsAmount;
                    temporary = 0;
                    coinSlider2.value = 0;
                    if (thisTile.boostObject != null)
                    {
                        for (int i = 0; i < land.ownedObjects.Count; i++)
                        {
                            if (land.ownedObjects[i] == thisTile.boostObject.type)
                            {
                                coins += thisTile.boost;
                            }
                        }
                    }
                }
                if (!objects.HasTile(CellPos))
                {
                    Destroy(coinSlider2.gameObject);
                    yield break;
                }
                if (objects.HasTile(CellPos))
                {
                    if (objects.GetTile(CellPos) == construction)
                    {
                        Destroy(coinSlider2.gameObject);
                        yield break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public IEnumerator Taxes(float seconds)
    {
        float temporary = 0;
        while (true)
        {
            temporary += Time.deltaTime;
            if (temporary >= seconds)
            {
                temporary = 0;
                foreach (TileBase tile in land.ownedLand)
                {
                    coins -= 30;
                }
                for (int i = 0; i < land.ownedObjects.Count; i++)
                {
                    for (int k = 0; k < land.objectTypes.Length; k++)
                    {
                        if (land.ownedObjects[i] == land.objectTypes[k].type && land.objectTypes[k].boostingTile != null)
                        {
                            for (int j = 0; j < land.ownedObjects.Count; j++)
                            {
                                if (land.ownedObjects[j] == land.objectTypes[k].boostingTile.type)
                                {
                                    coins -= land.objectTypes[k].boostTaxes;
                                }
                            }
                        }
                    }
                    for (int j = 0; j < buildingTypes.Length; j++)
                    {
                        if (land.ownedObjects[i] == buildingTypes[j].type)
                        {
                            coins -= buildingTypes[j].tax;
                        }
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
