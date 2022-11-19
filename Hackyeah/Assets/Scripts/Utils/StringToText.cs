using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StringToText : MonoBehaviour
{
    [SerializeField] SO_String SO_String;
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
        textMeshPro.text = SO_String.String;
    }
}
