using UnityEngine;
using UnityEngine.EventSystems;
//using Lean.Touch;

public class ClickOnTile : MonoBehaviour
{
    public delegate void CardAction();
    public static event CardAction OnCard;
    
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] MapManager mapManager;
    //[SerializeField] TouchInput touchInput;
    [SerializeField] CameraControlNet cameraControl;

    [SerializeField] SO_Bool isInBuildMode;

    [SerializeField] SO_Integer CurrentBuildingIndex;
    [SerializeField] Color mockColor;

    [Header("Prefabs")]
    [SerializeField] GameObject[] allPossibleBuildings;
    GameObject Building = null;
    GameObject Follower = null;

    GameObject selectedTile;
    GameObject oldTile;
    TileData tileData;
    TileData oldTileData;

    Vector3 flooredPosition;

    bool followerActive = false;

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
            if(Follower != null){Follower.transform.position = flooredPosition;}
            //Debug.Log(hit.transform.tag);
            
            if(isInBuildMode.boolvalue == true)
            {
                if(hit.transform.tag == "Tiles")
                {
                    DestroyFollower();
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
                            //CameraEnable();
                            Debug.Log("tile changed");
                            oldTileData.Deselcted();
                            oldTile = selectedTile;
                        }
                    }
                    else
                    {
                        //CameraEnable(); mobile
                        Debug.Log("tile changed from null");
                        DestroyMock();
                        oldTile = selectedTile;
                    }  
                }
                else if(hit.transform.tag == "Respawn")
                {
                    //CameraEnable(); //mobile
                    DestroyMock();
                    DisplayFollower(flooredPosition);
                }    
            }    
        }
        else
        {
            //CameraEnable();
            DestroyMock();
            DestroyFollower(); //mobile
        }
        } //eventcheck
    }

    void DisplayFollower(Vector3 mousePosition)
    {
        if(followerActive == false)
        {
            Follower = Instantiate(allPossibleBuildings[CurrentBuildingIndex.Integer], mousePosition, Quaternion.identity) as GameObject;
            //Follower.transform.SetParent(gameObject.transform);
            Mockify(Follower);
            followerActive = true;
        }
    }

    public void DestroyFollower()
    {
        Destroy(Follower);
        followerActive = false;
    }

    public void CameraEnable()
    {
        //if(cameraControl.enabled == false) { cameraControl.enabled = true; }//mobile
    }
    
    void PlaceBuilding()
    {
        //cameraControl.enabled = false; //disable camera on mobile
        DisplayMock();
        //Debug.Log("Place building works"); //it does, but sometimes input get mouse button doesn't

        if(/*touchInput.isTapped == true ||*/ Input.GetMouseButtonDown(0))
        {
            Debug.Log("Should place");
            InstantiatePrefab(Quaternion.Euler(0, RandomRotationValue(), 0), true);
            OnGetMouseButtonDown(false);
            mapManager.allPositions.Add(selectedTile.transform.position);
            DestroyMock();
            Destroy(selectedTile);
            //cameraControl.enabled = true; //mobile
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
        if(isBuilding == true) {Building.GetComponent<ParticleSystem>().Play();}
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
        //touchInput.isTapped = false;
        if(OnCard != null) {OnCard();}
    }

    int RandomRotationValue()
    {
        int[] rotation = {0, 60, -60};

        int angle = rotation[Random.Range(0 , rotation.Length)];

        return angle;
    }
}
