using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorebar : MonoBehaviour
{
    [SerializeField] SO_Integer panPlebanScore;

    [SerializeField] GameObject plebanPoint_1;
    [SerializeField] GameObject plebanPoint_2;
    [SerializeField] GameObject plebanPoint_3;
    [SerializeField] GameObject plebanPoint_4;

    [SerializeField] GameObject panPoint_1;
    [SerializeField] GameObject panPoint_2;
    [SerializeField] GameObject panPoint_3;
    [SerializeField] GameObject panPoint_4;

    //int points = panPlebanScore.Integer;

    // Update is called once per frame
    void Update()
    {
        CompareAndTranslate();
    }

    void CompareAndTranslate()
    {
        /*int points = panPlebanScore.Integer;
        switch (points)
        {
            case -4:
            Console.WriteLine("Monday");
            break;
            case -3:
            Console.WriteLine("Tuesday");
            break;
            case -2:
            Console.WriteLine("Wednesday");
            break;
            case -1:
            Console.WriteLine("Thursday");
            break;
            case 0:
            Console.WriteLine("Friday");
            break;
            case 1:
            Console.WriteLine("Saturday");
            break;
            case 2:
            Console.WriteLine("Sunday");
            break;
            case 3:
            Console.WriteLine("Sunday");
            break;
            case 4:
            Console.WriteLine("Sunday");
            break;
        }*/
    }

    /*void ResetAll()
    {
        plebanPoint_1
        plebanPoint_2
        plebanPoint_3
        plebanPoint_4


    }*/
}
