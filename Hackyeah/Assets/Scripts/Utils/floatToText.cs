using UnityEngine;
using UnityEngine.UI;

public class floatToText : MonoBehaviour
{
    [SerializeField] SO_Float SO_Float;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        TextChange();
    }

    void TextChange()
    {
        text.text = SO_Float.Float.ToString();
    }
}
