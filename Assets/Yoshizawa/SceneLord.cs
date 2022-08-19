using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLord : MonoBehaviour
{
    [SerializeField] string _lordScene;
    [SerializeField] float _time;
    AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void LordScene(string cor_name)
    {
        StartCoroutine(LordInterval(cor_name));
    }

    IEnumerator LordInterval(string sceneName)
    {
        _audio.Play();
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(sceneName);
    }
}
