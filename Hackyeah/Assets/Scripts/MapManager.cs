using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //On Awake spawn some basic obstacles at x random positions. Prefabs are basic placables but with hexraycastcheck disabled.
    [SerializeField] GameObject[] prefabs;
    [SerializeField] SO_Integer numberOfTilesToSpawn;
    public List<Vector3> allPositions = new List<Vector3>();
    [SerializeField] Vector3[] selectedPositionsToUseThisTime;

    int randomPositionIndex = 1;
    int randomPrefabIndex = 1;

    void Awake()
    {
        selectedPositionsToUseThisTime = new Vector3[numberOfTilesToSpawn.Integer];
        GetSelectedPositions();
        Spawn();
    }

    void Spawn()
    {
        foreach (var position in selectedPositionsToUseThisTime)
        {
            randomPrefabIndex = Random.Range(0, prefabs.Length);

            var obstacle = Instantiate(prefabs[randomPrefabIndex], position, Quaternion.identity) as GameObject;
            obstacle.GetComponent<AudioSource>().enabled = false;
            obstacle.GetComponent<CheckBordersHex>().isGainer = false;
            obstacle.name = "Obstacle at " + position.ToString();
            SearchChild(obstacle, 0);
            obstacle.transform.SetParent(gameObject.transform);
            //obstacle.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
        }
    }

    void GetSelectedPositions()
    {        
        for (int i = 0; i < numberOfTilesToSpawn.Integer; i++)
        {
            randomPositionIndex = Random.Range(0, allPositions.Count);
            
            selectedPositionsToUseThisTime[i] = allPositions[randomPositionIndex];
            allPositions.RemoveAt(randomPositionIndex);
        }
    }

    void SearchChild(GameObject gameObj, int index)
    {
        if(gameObj.transform.childCount > 0)
        {
            GameObject Child = gameObj.transform.GetChild(index).gameObject;
            Child.name = "Obstacle";
        }
    }
}
