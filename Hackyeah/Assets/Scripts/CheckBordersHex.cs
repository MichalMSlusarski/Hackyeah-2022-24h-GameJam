using UnityEngine;

public class CheckBordersHex : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] SO_Integer numberOfConnectedTowers;
    float rayRange = 1f;
    [SerializeField] GameObject tilePrefab;
    public Placable placable;
    [HideInInspector] public bool isGainer = true;

    RaycastHit hit;
    
    const int fullCircle = 360;
    const int nGon = 6;

    void Start()
    {
        placable.SubPrice();
        BorderCheck();
    }

    void BorderCheck()
    {
        Quaternion q = Quaternion.AngleAxis(fullCircle/nGon, Vector3.up);
        Quaternion q2 = Quaternion.AngleAxis(fullCircle/(nGon/2), Vector3.up);
        Quaternion q3 = Quaternion.AngleAxis(fullCircle/(nGon/3), Vector3.up);
        Vector3 d = transform.right * rayRange;

        Vector3 ray1 = q * d;
        Vector3 ray2 = q * -d;
        Vector3 ray3 = q2 * d;
        Vector3 ray4 = q2 * -d;
        Vector3 ray5 = q3 * d;
        Vector3 ray6 = q3 * -d;

        Rays(ray1);
        Rays(ray2);
        Rays(ray3);
        Rays(ray4);
        Rays(ray5);
        Rays(ray6); 
    }

    void Place(Vector3 rayPosition)
    {
        var tile = Instantiate(tilePrefab, rayPosition, Quaternion.identity) as GameObject;
        tile.name = "Tile at " + rayPosition.ToString();
        tile.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
    }

    void Rays(Vector3 rayTarget)
    {
        if(Physics.Raycast(gameObject.transform.position, rayTarget, out hit, rayRange, layerMask) == false)
        {
            Vector3 rayPosition = gameObject.transform.position + (rayTarget) * rayRange;
            Place(rayPosition);
            Debug.DrawRay(transform.position, rayTarget, Color.red, 1000f);
        }
        else
        {
            Debug.DrawRay(transform.position, rayTarget, Color.green, 1000f);
            
            if(isGainer) //makes initial spawn possible
            {
            
            if(hit.transform.name == "Obstacle")//else if(hit.transform.tag == "Finish")
            {
                Debug.Log("Tower Connected");
                hit.transform.name = "Default";
                //hit.transform.tag = "Untagged";
                numberOfConnectedTowers.Integer++;
            }

            if(hit.transform.gameObject.GetComponent<Placable>() != null)
            {
                Placable hitPlacable = hit.transform.gameObject.GetComponent<Placable>();

                if(hitPlacable.productType == placable.bonusForTileWithThisProductType)
                {
                    Debug.Log(transform.name + " " + rayTarget + " matched for bonus");
                    Debug.DrawRay(transform.position, rayTarget, Color.yellow, 1000f);
                    placable.AddPoint();
                }
                else if(hitPlacable.productType == placable.negativeForTileWithThisProductType)
                {
                    Debug.Log(transform.name + " " + rayTarget + " matched for bonus");
                    Debug.DrawRay(transform.position, rayTarget, Color.blue, 1000f);
                    placable.SubPoint();
                }
            }
            }
        }
    }
}
