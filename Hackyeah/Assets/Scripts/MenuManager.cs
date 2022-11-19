using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] string Tag = "Menu";
    [SerializeField] bool isTile = false;
    
    void Awake()
    {
        FindAllitems();
    }

    public void MoveItemIn()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<MenuItem>().MoveIn();
        }
    }

    public void MoveItemOut()
    {
        if(isTile) {DeselectAllItems();}
        
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<MenuItem>().MoveOut();
        }
    }

    void FindAllitems()
    {
        items = GameObject.FindGameObjectsWithTag(Tag);

        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
    }

    void DeselectAllItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<TileCard>().Deselect();
        }
    }
}
