using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    float lerpTime = 0.3f;
    bool isSelected = false;

    [SerializeField] Vector3 restPosition;
    [SerializeField] Vector3 startPosition;
    RectTransform rectTransform;
    RectTransform startRectTransform;

    [SerializeField] GameObject startObject;

    void Awake() 
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startRectTransform = startObject.GetComponent<RectTransform>();
        restPosition = rectTransform.anchoredPosition;//gameObject.transform.position;
        startPosition = startRectTransform.anchoredPosition;
        
        restPosition = gameObject.transform.position;
        startPosition = startObject.transform.position;
    }

    public void MoveOut()
    {
        //Debug.Log("Moved out");
        rectTransform.DOMove(startPosition, lerpTime, false);
        WaitManager.Wait(lerpTime - 0.2f, () => {this.gameObject.SetActive(false);});
    }

    public void MoveIn()
    {
        this.gameObject.SetActive(true);
        rectTransform.anchoredPosition = startPosition;
        rectTransform.DOMove(restPosition, lerpTime, false);
    }
}
