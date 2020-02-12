using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlertController : MonoBehaviour
{

    float delayTime;
    bool waiting;
    private void Start()
    {
        waiting = true;
        delayTime = Time.time + 3;
    }
    private void Update()
    {
        if (waiting && Time.time > delayTime) {
            waiting = false;
            StartCoroutine(LoadYourAsyncScene());

        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
