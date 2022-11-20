using UnityEngine;
using System.Linq;

public class GainCalculator : MonoBehaviour
{
    CheckBordersHex checkBordersHex;
    [SerializeField] SO_Integer gain;
    [SerializeField] SO_Integer currentPrice;
    [SerializeField] GameObject canvas;
    new Camera camera;

    RaycastHit hit;  
    
    const int fullCircle = 360;
    const int nGon = 6;
    float rayRange = 1f;
    
    void Start()
    {
        gain.Integer = 0;
        checkBordersHex = gameObject.GetComponent<CheckBordersHex>();
        CalculateGains();
        camera = Camera.main;
    }

    void Update()
    {
        canvas.transform.LookAt(camera.transform);
    }

    void CalculateGains()
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

    void Rays(Vector3 rayTarget)
    {
        if(Physics.Raycast(gameObject.transform.position, rayTarget, out hit, rayRange, checkBordersHex.layerMask))
        {
            if(hit.transform.gameObject.GetComponent<Placable>() != null)
            {
                Placable hitPlacable = hit.transform.gameObject.GetComponent<Placable>();
                
                if (new[] {checkBordersHex.placable.bonusForTileWithThisProductType}.Contains(hitPlacable.productType)) {Debug.Log("IMPORTANT " + hitPlacable.productType + " " + checkBordersHex.placable.bonusForTileWithThisProductType);}

                if(hitPlacable.productType == checkBordersHex.placable.bonusForTileWithThisProductType)
                {
                    
                    gain.Integer = gain.Integer + checkBordersHex.placable.bonusValue;// - currentPrice.Integer;
                }
            } 
        }     
    }
}
