using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool Deleting = false;
    public bool Buying = true;

    public bool cantBuildDestroy = false;

    public bool[] buildings;

    public void SetToDeleting()
    {
        Deleting = true;
        Buying = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
        }
    }
    public void SetToFarmBuilding()
    {
        Deleting = false;
        Buying= false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
            if (i == 0)
            {
                buildings[i] = true;
            }
        }
    }
    public void SetToLumberBuilding()
    {
        Deleting = false;
        Buying = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
            if (i == 1)
            {
                buildings[i] = true;
            }
        }
    }
    public void SetToMineBuilding()
    {
        Deleting = false;
        Buying = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
            if (i == 2)
            {
                buildings[i] = true;
            }
        }
    }
    public void SetToHouseBuilding()
    {
        Deleting = false;
        Buying = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
            if (i == 3)
            {
                buildings[i] = true;
            }
        }
    }
    public void SetToBuyingLand()
    {
        Deleting = false;
        Buying = true;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
        }
    }
}
