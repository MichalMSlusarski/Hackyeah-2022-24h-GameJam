using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
//using Lean.Touch;

public class CameraControlNet : MonoBehaviour
{
    [Header("Settings:")]
    [SerializeField] float rotSpeed = 10f;
    [SerializeField] float zoomTime = 0.3f;
    [SerializeField] float lerpTime = 0.3f;

    [Header("Zooming distances:")]
    [SerializeField] float[] zoomFOVs;

    [SerializeField] LayerMask layerMask;

    [Header("Distance thresholds:")]
    [SerializeField] float maxMoveDistance = 5f;
    [SerializeField] float minMoveDistance = 0.9f;
    
    Vector3 mouseClickPosition;
    
    float startFOV;
    [Header("References:")]
    [SerializeField] SO_Integer zoomIndex;
    [SerializeField] Camera cam;
    //[SerializeField] TouchInput touchInput;

    float mouseX, mouseY;

    void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        cam = Camera.main;
        startFOV = cam.fieldOfView;
        zoomFOVs[0] = startFOV;
    }

    void Update()
    {
        Rotation();
        Zoom();
        Move();
    }

    void Rotation() //implementing touch here gonna be hard
    {
        if(Input.GetMouseButton(2))
        {
            mouseX += Input.GetAxis("Mouse X") * rotSpeed;
            transform.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }

    void Move()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if(Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                mouseClickPosition = new Vector3(hit.point.x, 0, hit.point.z);

                float distance = Vector3.Distance(transform.position, mouseClickPosition);
               // Debug.Log("MOVE");

                if(distance <= maxMoveDistance && distance >= minMoveDistance)
                {
                    if(Input.GetMouseButtonUp(0))// || touchInput.isTouchCancelled == true) //UP
                    {
                        float smallTime = 0.2f;
                        //touchInput.isTouchCancelled = false;
                        //touchInput.isTapped = false;
                        WaitManager.Wait(smallTime, () => {DOTween.KillAll();});
                    }
                    else if(Input.GetMouseButton(0)) //|| touchInput.isTapped == true) 
                    {
                        gameObject.transform.DOMove(mouseClickPosition, lerpTime, false).SetEase(Ease.Linear);
                        //touchInput.isTapped = false;
                    }
                }
            }
        }     
    }

    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)// || touchInput.pinchedIn) // na przód
        {
            zoomIndex.Integer = 1;
            ZoomSwitch(zoomIndex.Integer);

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // || touchInput.pinchedOut) // w tył
        {
            zoomIndex.Integer = 0;
            ZoomSwitch(zoomIndex.Integer);
        }
    }

    void ZoomSwitch(int FOV)
    {
        cam.DOFieldOfView(zoomFOVs[FOV], zoomTime);
    }
}
