using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardYeeter : MonoBehaviour
{
    [SerializeField] ClickOnTile clickOnTile;
    GameObject[] activeCards;
    
    void Start()
    {
        ClickOnTile.OnCard += Yeet;
    }

    void SearchForAllActiveCards()
    {
        
    }

    void Yeet()
    {
        ClickOnTile.OnCard -= Yeet;
    }
}
