using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntToText : MonoBehaviour
{
    [SerializeField] SO_Integer Points;
    [SerializeField] bool padWithZeros = false;
    TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TextChange();
    }

    void TextChange()
    {
        if(padWithZeros)
        {
            textMeshPro.text = Points.Integer.ToString("0000");
        }
        else
        {
            textMeshPro.text = Points.Integer.ToString();
        }
    }
}
