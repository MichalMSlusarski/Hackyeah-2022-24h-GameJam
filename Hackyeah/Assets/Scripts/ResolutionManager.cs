using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject[] menus;
    Vector2 nativeResolution = new Vector2(1920f, 1080f); //resolution in which the game was being made, the most defult one
    
    //script priority 0
    void Awake()
    {
        //underCanvas.SetActive(false);
        SetResolution();
    }

    void Scale(Vector2 nativeResolution, Vector2 resolutionReference)
    {
        Vector2 scalingVector = resolutionReference / nativeResolution;
        
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].transform.localScale = menus[i].transform.localScale * scalingVector;
            Debug.Log(menus[i].transform.localScale + " " + scalingVector);
        }
    }

    void SetResolution()
    {
        Resolution resolution = Screen.currentResolution;
        Vector2 resolutionReference = new Vector2(resolution.width, resolution.height);
        //Vector2 canvasResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;
        canvas.GetComponent<CanvasScaler>().referenceResolution = resolutionReference;
        //canvasResolution = resolutionReference;
        Debug.Log(resolution + " " + resolutionReference); //canvasResolution);

        if(canvas.GetComponent<CanvasScaler>().referenceResolution != null) {canvas.SetActive(true);} //checks if resolution has been assigned correctly

        Scale(nativeResolution, resolutionReference);
    }
}
