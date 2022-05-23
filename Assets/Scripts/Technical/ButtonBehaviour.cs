using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool Deleting = false;
    public bool Buying = true;

    public bool cantBuildDestroy = false;

    public bool[] buildings;

    public GameObject[] buttonHolders;

    private void Start()
    {
        for (int i = 0; i < buttonHolders.Length; i++)
        {
            buttonHolders[i].SetActive(false);
        }
    }

    public void SetToDeleting()
    {
        Deleting = true;
        Buying = false;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
        }
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
    }
    public void SetToBuying()
    {
        Deleting = false;
        Buying = true;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i] = false;
        }
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
    }
    public void SetSlotHolder(int i)
    {
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
        buttonHolders[i].SetActive(true);
    }

    public void SetBuilding(int i)
    {
        Deleting = false;
        Buying = false;
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
        cantBuildDestroy = false;
        for (int j = 0; j < buildings.Length; j++)
        {
            buildings[j] = false;
        }
        buildings[i] = true;
    }
}
