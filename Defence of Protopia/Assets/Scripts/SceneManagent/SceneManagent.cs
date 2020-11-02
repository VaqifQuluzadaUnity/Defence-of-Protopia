using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Faqan Aliyev
public class SceneManagent : MonoBehaviour
{

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
