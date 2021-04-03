using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public HandlerBase CurrentScene { get; set; }

    public void ChangeScene(string name) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        // topHandlerBase = handlerBase;
    }
}
