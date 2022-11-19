using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] SO_Integer sceneToLoad;
    [SerializeField] int desiredNextSceneIndex;
   // [SerializeField] AudioSource audioSource;
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextSceneAtIndex()
    {
        SceneManager.LoadScene(sceneToLoad.Integer, LoadSceneMode.Single);
    }

    public void SetNextSceneIndexTo()
    {
        sceneToLoad.Integer = desiredNextSceneIndex;
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Outline()
    {
        Outline outline = this.gameObject.GetComponent<Outline>();
        //if(outline != null)
        outline.enabled = false ? outline.enabled = true : outline.enabled = false;  
    }

    public void PlayClickSound()
    {

    }
}
