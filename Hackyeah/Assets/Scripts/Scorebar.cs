using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorebar : MonoBehaviour
{
    [SerializeField] Image scorebarPleban;
    [SerializeField] Image scorebarPan;
    [SerializeField] SO_Integer panPlebanScore;

    int maxScore = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CompareAndTranslate();
    }

    void CompareAndTranslate()
    {
        if(panPlebanScore.Integer <= 0)
        {
            float currentScore = (panPlebanScore.Integer / maxScore);
            scorebarPan.fillAmount = currentScore;
            Debug.Log(currentScore);
        }
        else if(panPlebanScore.Integer >= 0)
        {
            float currentScore = (panPlebanScore.Integer / maxScore) * -1;
            scorebarPan.fillAmount = currentScore;
            Debug.Log(currentScore);
        }
    }
}
