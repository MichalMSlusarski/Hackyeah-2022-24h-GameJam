using UnityEngine;

public class Rotate : MonoBehaviour
{
    //[SerializeField] Vector3 rotationAngle;
    [SerializeField] float rotationsPerMinute = 4f;

    void Update()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
    }
    

}
