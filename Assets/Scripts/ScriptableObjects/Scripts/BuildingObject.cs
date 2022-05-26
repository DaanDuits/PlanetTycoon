using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Buildings", order = 1)]
public class BuildingObject : ScriptableObject
{
    public TileBase type;
    public TileBase[] placeableTerrain;

    public int price;
    public float buildTime;
    public int coinsAmount;
    public float coinsTime;
    public bool onObjects;

    public bool isProduction;
    public ProduceObject[] produce;

    public int value;
    public int neededValue;

    public BuildingObject boostingTile;
    public int boostTaxes;
    public BuildingObject boostObject;
    public int boost;

    public int tax;
}
