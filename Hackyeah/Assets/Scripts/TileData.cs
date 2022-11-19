using UnityEngine;

public class TileData : MonoBehaviour
{
    public bool isFreeToBuild = false;
    public bool isMockOn = false;
    [HideInInspector] public Material material;
    Renderer tileRenderer;
    [HideInInspector] public Color defaultColor;

    void Awake()
    {
        tileRenderer = gameObject.GetComponent<Renderer>();
        material = tileRenderer.material;
        defaultColor = material.color;
    }

    public void Deselcted()
    {
        if(material.color != defaultColor)
        {
            material.color = defaultColor;
        }
        if(isFreeToBuild == true && isMockOn == true)
        {
            CleanChildren();
        }
    }

    public void RenderOn()
    {
        tileRenderer.enabled = true;
    }

    void CleanChildren()
    {
        if(gameObject.transform.childCount > 0)
        {
            GameObject Child = gameObject.transform.GetChild(1).gameObject; //the index of a child is very important. Set default to 1, because index 0 is designated for a child component for visuals
            Destroy(Child);
            isMockOn = false;
        }
    }
}
