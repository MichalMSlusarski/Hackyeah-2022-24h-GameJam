using UnityEngine;

[System.Flags]
public enum Products
{
    Fields = 1 << 0,
    Woods = 1 << 1,
    Rocks = 1 << 2,
    Windmill = 1 << 3,
    Mine = 1 << 4,
    Town = 1 << 5,
    Sawmill = 1 << 6,
}

public class Placable : MonoBehaviour
{
    public Products productType;
    public Products bonusForTileWithThisProductType; //1001
    
    [SerializeField] SO_Integer villagePoints;
    [SerializeField] SO_Integer productPoints;
    [SerializeField] SO_Integer currentPrice;
    bool productByTurn = false;
    public int bonusValue = 1;
    

    void Start()
    {
        ClickOnTile.OnCard += OnUpdate;
        //Debug.Log(bonusForTileWithThisProductType);
    }

    void OnUpdate()
    {
        
    }

    void OnDestroy()
    {
        ClickOnTile.OnCard -= OnUpdate;
    }

    public void SubPrice()
    {
        productPoints.Integer = productPoints.Integer - currentPrice.Integer;
    }

    public void AddPoint()
    {
        if(productByTurn == false) {villagePoints.Integer = villagePoints.Integer + bonusValue;}
    }
}
