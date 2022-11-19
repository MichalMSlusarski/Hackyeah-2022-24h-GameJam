using UnityEngine;
using DG.Tweening;

public class TileVisual : MonoBehaviour
{
    Vector3 initialScale = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 desiredScale = new Vector3(1f, 1f, 0.25f);
    [SerializeField] float lerpTime = 0.1f;

    void Start()
    {
        gameObject.transform.localScale = initialScale;
        gameObject.transform.DOScale(desiredScale, lerpTime);
    }
}
