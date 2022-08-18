using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAttachment : MonoBehaviour
{
    public delegate void MonoEvent();
    MonoEvent _onUpdateCallback;

    float _time = 0;

    private void Awake()
    {
        GameManager.Instance.Setup(this);
    }
    /// <summary>
    /// GameManager��Update�������R�[���o�b�N�ɒǉ�m
    /// </summary>
    /// <param name="e"></param>
    public void SetupCallback(MonoEvent e)
    {
        _onUpdateCallback = e;
    }

    private void Update()
    {
        _onUpdateCallback.Invoke();
    }
    private void OnDestroy()
    {
        //������U����
        GameManager.Instance.Destroy();
    }
}
