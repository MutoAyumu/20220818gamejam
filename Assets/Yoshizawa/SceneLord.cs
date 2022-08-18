using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLord : MonoBehaviour
{
    [SerializeField] string _lordScene;

    public void LordScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
