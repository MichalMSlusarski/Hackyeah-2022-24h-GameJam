using UnityEngine;
using DG.Tweening;
//using UnityEngine.EventSystems;

public class TileCard : MonoBehaviour
{
    string defaultTag = "Card";
    string selectedTag = "SelectedCard";
    float desiredSize = 2.2f;
    float lerpTime = 0.4f;
    float initialScale = 2f;
    bool isSelected = false;

    [SerializeField] int thisBuildingIndex;
    [SerializeField] int price;
    [SerializeField] SO_Integer currentBuildingIndex;
    [SerializeField] SO_Bool isBuildMode;

    [SerializeField] ClickOnTile clickOnTile;
    [SerializeField] SO_Integer currentPrice;

    string thisCardName;

    void Awake()
    {
        thisCardName = gameObject.name;

    }

    void Start()
    {
        ClickOnTile.OnCard += Deselect;       
    }
    
    public void Selected()
    {
            ClearSelection(thisCardName);

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
        clickOnTile.DestroyMock();

        gameObject.transform.DOScale(desiredSize, lerpTime);
    }

    public void Exit()
    {
        if(isSelected == false) {gameObject.transform.DOScale(initialScale, lerpTime);}
    }

    void OnDestroy()
    {
        ClickOnTile.OnCard -= Deselect;
        //ClickOnTile.OnCard -= AffordCheck;
        DOTween.KillAll();
    }
}
