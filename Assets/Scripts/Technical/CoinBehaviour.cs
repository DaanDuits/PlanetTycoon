using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    public BuyingLandBehaviour ownedLand;

    public Canvas canvas;
    public TileBase construction;

    public Slider coinSlider;

    public Text coinsText;
    public int coins = 0;
    public int startAmount;

    public float taxTime;

    public BuildingObjects[] buildingTypes;
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

    public IEnumerator generateMoneyOverTime(int coinsAmount, int posX, int posY, float seconds, BuildingObjects thisTile)
    {
        Vector3Int CellPos = objects.WorldToCell(new Vector3(posX, posY, 0));
        if(seconds != 0)
        {
            Slider coinSlider2 = Instantiate(coinSlider, new Vector2(posX + 0.5f, posY + 0.9f), Quaternion.identity, canvas.transform);
            coinSlider2.value = 0;
            coinSlider2.maxValue = seconds;
            float temporary = 0;
            while (true)
            {
                temporary += Time.deltaTime;
                coinSlider2.value += Time.deltaTime;
                if (temporary >= seconds)
                {
                    coins += coinsAmount;
                    temporary = 0;
                    coinSlider2.value = 0;
                    if (thisTile.boostObject.type != null)
                    {
                        for (int i = 0; i < ownedLand.ownedObjects.Count; i++)
                        {
                            if (ownedLand.ownedObjects[i] == thisTile.boostObject.type)
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
                foreach (TileBase tile in ownedLand.ownedLand)
                {
                    coins -= 30;
                }
                for (int i = 0; i < ownedLand.ownedObjects.Count; i++)
                {
                    for (int j = 0; j < buildingTypes.Length; j++)
                    {
                        if (ownedLand.ownedObjects[i] == buildingTypes[j].type)
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
