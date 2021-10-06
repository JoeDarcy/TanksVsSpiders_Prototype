using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = 0;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
