using UnityEngine;

[System.Flags]
public enum Products
{
    Wieza = 1 << 0,
    Chata = 1 << 1,
    Biala_Chata = 1 << 2,
    Plebania = 1 << 3,
    Kasztel = 1 << 4,
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
