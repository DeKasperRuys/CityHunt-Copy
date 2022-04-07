using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScenes : MonoBehaviour
{
   
    public void changeToMap()
    {
            SceneManager.LoadScene(2);

    }

    public void changeToMenu()
    {
        SceneManager.LoadScene(1);

    }
}
