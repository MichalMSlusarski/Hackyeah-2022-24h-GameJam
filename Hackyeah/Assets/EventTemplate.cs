using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EventTemplate : MonoBehaviour
{
    public Vector3 gdzie;
    public float duration;
    public SO_Integer villageScore;
    public int effect = 0;
    public bool pleban = false;
    [SerializeField] GameObject kwestieLewa;
    [SerializeField] GameObject kwestiePrawa;
    [SerializeField] Image lewaOsoba;
    [SerializeField] Image prawaOsoba;

    public Color fadeshade;
    
    void Start()
    {
        this.gameObject.transform.DOMove(gdzie, duration);
        lewaOsoba.DOColor(fadeshade, duration);
        prawaOsoba.DOColor(fadeshade, duration);
        //kwestiePrawa.DOScale(1, duration);
    }

    public void Effect()
    {
        if(pleban) {villageScore.Integer -= (villageScore.Integer / 10);}
        villageScore.Integer += effect;
    }

    
}
