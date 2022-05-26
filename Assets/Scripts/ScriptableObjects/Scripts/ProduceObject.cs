using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewProduce", menuName = "ScriptableObjects/Produce", order = 1)]
public class ProduceObject : ScriptableObject
{
    public GameObject type;
    public BuildingObject buildOn;
    public TileBase tileFind;
    public  int amount;
    public float time;

    public int tilePrice;

    public int price;

    public int i;
}
