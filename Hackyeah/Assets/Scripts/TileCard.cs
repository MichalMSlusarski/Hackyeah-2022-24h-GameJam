using UnityEngine;
using DG.Tweening;
//using UnityEngine.EventSystems;

public class TileCard : MonoBehaviour
{
    string defaultTag = "Card";
    string selectedTag = "SelectedCard";
    float desiredSize = 1.15f;
    float lerpTime = 0.4f;
    float initialScale = 1f;
    bool isSelected = false;

    [SerializeField] SO_Integer currentMoney;
    [SerializeField] int thisBuildingIndex;
    [SerializeField] int price;
    [SerializeField] SO_Integer currentBuildingIndex;
    [SerializeField] SO_Bool isBuildMode;
    [SerializeField] GameObject icons;
    [SerializeField] GameObject grayout;
    [SerializeField] ClickOnTile clickOnTile;
    [SerializeField] SO_Integer currentPrice;

    string thisCardName;

    void Awake()
    {
        thisCardName = gameObject.name;
        grayout.SetActive(true);
        icons.SetActive(false);
    }

    void Start()
    {
        ClickOnTile.OnCard += Deselect;       
        ClickOnTile.OnCard += AffordCheck;
    }

    void Update()
    {
        AffordCheck();
    }

    void AffordCheck()
    {
        if(CanAfford())
        {
            grayout.SetActive(false);
        }
        else
        {
            grayout.SetActive(true);
        }
    }
    
    public void Selected()
    {
        if(CanAfford())
        {
            ClearSelection(thisCardName);
            icons.SetActive(true);
            isSelected = true;
            Debug.Log("Selected");
            gameObject.transform.tag = selectedTag;
            gameObject.transform.DOScale(desiredSize, lerpTime);
            currentBuildingIndex.Integer = thisBuildingIndex;
            currentPrice.Integer = price;

            WaitManager.Wait(0.1f, () =>
            {
            isBuildMode.boolvalue = true;
            }); 
        }
    }

    void ClearSelection(string name)
    {
        if(Input.GetMouseButton(0)) //prevents from deselecting with other buttons
        {
            //Debug.Log("Diselected");
            GameObject[] selectedCards = GameObject.FindGameObjectsWithTag(selectedTag);

            for (int i = 0; i < selectedCards.Length; i++)
            {
                if(selectedCards[i].name != thisCardName)
                {
                    TileCard selectedTileCard = selectedCards[i].GetComponent<TileCard>();
                    selectedTileCard.Deselect();
                }
            }
        }   
    }

    public void Deselect()
    {
        isSelected = false;
        WaitManager.Wait(0.1f, () => 
        {
            gameObject.transform.tag = defaultTag;
            isBuildMode.boolvalue = false;
            Exit();
        });
    }

    public void Enter()
    {
        clickOnTile.DestroyFollower(); //+ destroyMock
        clickOnTile.DestroyMock();
        icons.SetActive(true);
        gameObject.transform.DOScale(desiredSize, lerpTime);
    }

    public void Exit()
    {
        if(isSelected == false) {gameObject.transform.DOScale(initialScale, lerpTime); icons.SetActive(false);}
    }

    bool CanAfford()
    {
        if(currentMoney.Integer >= price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDestroy()
    {
        ClickOnTile.OnCard -= Deselect;
        ClickOnTile.OnCard -= AffordCheck;
        DOTween.KillAll();
    }
}
