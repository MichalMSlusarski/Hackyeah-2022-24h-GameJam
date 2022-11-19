using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraControlNet cameraControlNet;
    [SerializeField] SO_Integer numberOfAvailableTiles;
    [SerializeField] SO_Integer numberOfTowersToConnect;
    [SerializeField] SO_Integer numberOfConnectedTowers;
    [SerializeField] SO_Integer currentPrice;
    [SerializeField] SO_Integer score;
    [SerializeField] SO_Bool buildMode;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] int maxTiles = 32;
    [SerializeField] int startingScore = 10;
    public bool isInfinite = false;

    void Awake()
    {
        buildMode.boolvalue = false;
        cameraControlNet.enabled = true;
        score.Integer = startingScore;
        numberOfAvailableTiles.Integer = maxTiles;
        numberOfConnectedTowers.Integer = 0;
        ClickOnTile.OnCard += UpdateScene;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        currentPrice.Integer = 0;
    }

    void UpdateScene()
    {
        numberOfAvailableTiles.Integer--;
    }
        
    void Update()
    {    
        WinCheck();
        IsPlayerBrokeCheck();
    }

    void IsPlayerBrokeCheck()
    {
        if(score.Integer <= 0)
        {
            WaitManager.Wait(0.1f, () => //dirty ass solution lol
            {
            if (score.Integer <= 0)
            {
                Win(false);
            }
            });
        }
    }

    void WinCheck()
    {
        if(isInfinite == false)
        {
            if(numberOfAvailableTiles.Integer <= 0)
            {
                Debug.Log("You run outta tiles!");

                if(numberOfConnectedTowers.Integer >= (numberOfTowersToConnect.Integer))
                {
                    Win(true);
                }
                else
                {
                    Win(false);
                }
            }
            else if(numberOfAvailableTiles.Integer > 0)
            {
                if(numberOfConnectedTowers.Integer >= (numberOfTowersToConnect.Integer))
                {
                    Win(true);
                }
            }
        }
    }

    void Win(bool hasWon)
    {
        Debug.Log(hasWon);
        if(hasWon) {winPanel.SetActive(true);}
        else {losePanel.SetActive(true);}
        cameraControlNet.enabled = false;
    }

    void OnDisable()
    {
        ClickOnTile.OnCard -= UpdateScene;
    }
}
