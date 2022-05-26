using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketBehaviour : MonoBehaviour
{
    ProductionBehaviour production => GameObject.Find("Controllers").transform.Find("ProductionController").GetComponent<ProductionBehaviour>();
    CoinBehaviour coins => GameObject.Find("Controllers").transform.Find("CoinController").GetComponent<CoinBehaviour>();

    public void SellItem(int i)
    {
        for (int j = 0; j < production.produceAmounts.Length; j++)
        {
            if (j == i && production.produceAmounts[i] != 0)
            {
                production.produceAmounts[i] -= 1;
                coins.coins += production.pObjects[i].price;
            }
        }
    }
    public void BuyItem(int i)
    {
        for (int j = 0; j < production.produceAmounts.Length; j++)
        {
            if (j == i && coins.coins != 0)
            {
                production.produceAmounts[i] += 1;
                coins.coins -= production.pObjects[i].price;
            }
        }
    }
}
