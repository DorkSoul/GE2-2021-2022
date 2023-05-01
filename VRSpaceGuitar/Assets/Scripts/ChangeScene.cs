using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour
{
    //function Used to Load Scene selected through a button
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
