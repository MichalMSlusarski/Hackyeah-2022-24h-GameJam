using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideToPoint : MonoBehaviour
{
    [SerializeField] SO_Integer score;
    [SerializeField] Image handle;
    [SerializeField] Color color_pan;
    [SerializeField] Color color_pleban;

    Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = score.Integer;

        if(score.Integer > 0)
        {
            handle.color = color_pan;
        }
        else
        {
            handle.color = color_pleban;
        }
    }
}
