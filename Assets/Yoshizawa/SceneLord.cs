using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLord : MonoBehaviour
{
    [SerializeField] string _loadScene;
    [SerializeField] float _time;

    public void LoadScene(string cor_name)
    {
        StartCoroutine(LoadInterval(cor_name));
    }

    IEnumerator LoadInterval(string sceneName)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(sceneName);
    }
}
