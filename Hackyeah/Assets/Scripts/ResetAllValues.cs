using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllValues : MonoBehaviour
{
    [SerializeField] SO_Integer[] allValues;
    
    
    void Start()
    {
        for (int i = 0; i < allValues.Length; i++)
        {
            allValues[i].Integer = 0;
        }
    }
}
