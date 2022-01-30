using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("the instance of the manager")]
    private static MenuManager instance;
    void Awake()
    {

        //Before enabling the object, we need to make sure that there are no other GameManagers.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("WillScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
