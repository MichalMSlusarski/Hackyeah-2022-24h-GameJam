using UnityEngine;
using DG.Tweening;

public class TileVisual : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    new Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        canvas.transform.LookAt(camera.transform);
    }
}
