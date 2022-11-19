using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardYeeter : MonoBehaviour
{
    [SerializeField] ClickOnTile clickOnTile;
    [SerializeField] List<GameObject> activeChoices = new List<GameObject>();
    
    void Start()
    {
        ClickOnTile.OnCard += Yeet;
    }

    void Yeet()
    {
        Debug.Log("Yeet");
        activeChoices[0].SetActive(false);
        activeChoices.RemoveAt(0);
    }

    void OnDestroy() 
    {
        ClickOnTile.OnCard -= Yeet;
    }
}
