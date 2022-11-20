using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardYeeter : MonoBehaviour
{
    [SerializeField] ClickOnTile clickOnTile;
    [SerializeField] SO_Integer panPlebanScore;
    [SerializeField] int turnsToEvent;
    [SerializeField] List<GameObject> activeChoices = new List<GameObject>();
    [SerializeField] List<GameObject> PanEventList = new List<GameObject>();
    [SerializeField] List<GameObject> PlebanEventList = new List<GameObject>();
    [SerializeField] float duration;
    [SerializeField] Vector3 trash;
    [SerializeField] GameObject gamePanel;
    //[SerializeField] Vector3 kartyPositon;
    //[SerializeField] GameObject karty;

    int turn;
    
    void Start()
    {
        ClickOnTile.OnCard += Yeet;
        //karty.transform.DOMove(kartyPositon, duration);
    }

    void Update()
    {
        if(activeChoices.Count == 0)
        {
            gamePanel.SetActive(true);
        }
    }

    void Yeet()
    {
        Debug.Log("Yeet");
        turn++;

        activeChoices[0].transform.DOMove(trash, duration);
        //activeChoices[0].SetActive(false);
        WaitManager.Wait(duration, () => 
        {
            activeChoices.RemoveAt(0);
        });
        
        if(turn >= turnsToEvent)
        {
            EventMe();
        }
    }

    void EventMe()
    {
        if(panPlebanScore.Integer >= 0)
        {
            PanEventList[0].SetActive(true);
            turn = 0;
        }
        else
        {
            PlebanEventList[0].SetActive(true);
            turn = 0;
        }
    }

    void OnDestroy() 
    {
        ClickOnTile.OnCard -= Yeet;
    }
}
