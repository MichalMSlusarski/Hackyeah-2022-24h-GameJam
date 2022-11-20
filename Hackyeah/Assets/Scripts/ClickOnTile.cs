using UnityEngine;
using UnityEngine.EventSystems;
//using Lean.Touch;

public class ClickOnTile : MonoBehaviour
{
    public delegate void CardAction();
    public static event CardAction OnCard;
    
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask ignoreLayer;
    //[SerializeField] MapManager mapManager;
    //[SerializeField] TouchInput touchInput;
    [SerializeField] CameraControlNet cameraControl;

    [SerializeField] SO_Bool isInBuildMode;

    [SerializeField] SO_Integer CurrentBuildingIndex;
    [SerializeField] Color mockColor;

    [Header("Prefabs")]
    [SerializeField] GameObject[] allPossibleBuildings;
    GameObject Building = null;

    GameObject selectedTile;
    GameObject oldTile;
    TileData tileData;
    TileData oldTileData;

    Vector3 flooredPosition;

    void Update()
    {
        MouseOverCheck();
    }
    
    void MouseOverCheck()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Touch touch = Input.GetTouch(0); ray = Camera.main.ScreenPointToRay(touch.positon);  

        if(!EventSystem.current.IsPointerOverGameObject()) {
        if(Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            flooredPosition = new Vector3(hit.point.x, 0, hit.point.z);
            //Debug.Log(hit.transform.tag);
            
            if(isInBuildMode.boolvalue == true)
            {
                if(hit.transform.tag == "Tiles")
                {
                    selectedTile = hit.transform.gameObject;
                    tileData = selectedTile.GetComponent<TileData>();
                
                    if(oldTile != null)
                    {
                        oldTileData = oldTile.GetComponent<TileData>();
                        //Debug.Log(oldTile.name + " old tile");
                        //Debug.Log(selectedTile.name + " selected tile");
                    
                        if(oldTile.name == selectedTile.name)
                        {
                            Debug.Log("tile didn't change");
                            PlaceBuilding();
                        }
                        else
                        {

                            Debug.Log("tile changed");
                            oldTileData.Deselcted();
                            oldTile = selectedTile;
                        }
                    }
                    else
                    {

                        Debug.Log("tile changed from null");
                        DestroyMock();
                        oldTile = selectedTile;
                    }  
                }
                else if(hit.transform.tag == "Respawn")
                {

                    DestroyMock();

                }    
            }    
        }
        else
        {
            DestroyMock();
        }
        }
    }
    
    void PlaceBuilding()
    {
        DisplayMock();
        Debug.Log("Mock Displayed");

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Should place");
            InstantiatePrefab(Quaternion.Euler(0, RandomRotationValue(), 0), true);
            OnGetMouseButtonDown(false);
            DestroyMock();
            Destroy(selectedTile);
        }   
    }

    void DisplayMock()
    {
        if(tileData.isMockOn == false && tileData.isFreeToBuild == true)
        {
            InstantiatePrefab(Quaternion.identity, false);
            Building.transform.SetParent(selectedTile.transform);
            Mockify(Building);
            Building.GetComponent<GainCalculator>().enabled = true;
            
            tileData.isMockOn = true;
        }
    }

    void Mockify(GameObject mockified) //this method makes normal building tiles their mocks
    {
        mockified.GetComponent<AudioSource>().enabled = false;
        mockified.GetComponent<CheckBordersHex>().enabled = false;
            
        if(mockified.transform.childCount > 0)
        {
            GameObject Child = mockified.transform.GetChild(0).gameObject;
            //Child.GetComponent<Renderer>().material.color = mockColor;
            Child.layer = ignoreLayer;
        }
    }

    void InstantiatePrefab(Quaternion rotation, bool isBuilding)
    {
        Building = Instantiate(allPossibleBuildings[CurrentBuildingIndex.Integer], selectedTile.transform.position, rotation) as GameObject; //Euler(0, RandomRotationValue(), 0)Quaternion.identity
        //if(isBuilding == true) //{//Building.GetComponent<ParticleSystem>().Play();}
        //Building.transform.SetParent(selectedTile.transform);
    }

    public void DestroyMock()
    {
        if(selectedTile != null)
        {
            tileData.Deselcted();
        }    
    }

    void OnGetMouseButtonDown(bool freetobuild)
    {
        tileData.Deselcted();
        oldTileData.Deselcted();
        isInBuildMode.boolvalue = false;
        tileData.isFreeToBuild = freetobuild;
        if(OnCard != null) {OnCard();}
    }

    int RandomRotationValue()
    {
        int[] rotation = {0, 60, -60};

        int angle = rotation[Random.Range(0 , rotation.Length)];

        return angle;
    }
}
