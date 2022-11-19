using UnityEngine;

public class SetRandomObjectActive : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    int randomIndex = 0;
    //GainCalculator gainCalculator = null;

    void Start()
    {
        //gainCalculator = gameObject.GetComponent<GainCalculator>();
        //if(gainCalculator.enabled == false) {Set();}
        Set();
    }

    void Set()
    {
        randomIndex = Random.Range(0, objects.Length);
        
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        
        objects[randomIndex].SetActive(true);
    }
}
