using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLord : MonoBehaviour
{
    [SerializeField] string _lordScene;
    [SerializeField] float _time;

    public void LordScene(string cor_name)
    {
        StartCoroutine(LordInterval(cor_name));
    }

    IEnumerator LordInterval(string sceneName)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(sceneName);
    }
}
