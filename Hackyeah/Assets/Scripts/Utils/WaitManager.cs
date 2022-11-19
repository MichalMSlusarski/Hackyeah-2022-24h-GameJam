using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitManager : MonoBehaviour
{
    static WaitManager instance = null;
    static WaitManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("Waiter").AddComponent<WaitManager>();
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    IEnumerator WaitRoutine(float duration, System.Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }

    public static void Wait(float seconds, System.Action callback)
    {
        Instance.StartCoroutine(Instance.WaitRoutine(seconds, callback));
    }
}
