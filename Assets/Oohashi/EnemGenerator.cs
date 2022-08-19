using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemGenerator : MonoBehaviour
{
    public bool _canEnemy = false;

    [SerializeField] Transform[] _spawnPos = default;
    [SerializeField] GirlsStatusManager[] _testO;
    [SerializeField] float _timeLimit = 2f;
    float _timer = 0f;
    List<Transform> _posList = new List<Transform>();

    bool _isPause;
    void Start()
    {
        foreach(var t in _spawnPos)
        {
            //List‚É‘S•”’Ç‰Á
            _posList.Add(t);
        }

        Test(_posList.Count);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }

    void Test(int n)
    {
        var count = 0;

        for(int i = 0; i < n; i++)
        {
            if (count == _posList.Count)
            {
                break;
            }

            if (_posList.Count == 0)
            {
                break;
            }

            var r1 = Random.Range(0, _posList.Count - 1);

            var t = _posList[r1];

            var r2 = Random.Range(0, _testO.Length);

            var o = Instantiate(_testO[r2]);
            o.Set(this, t);
            o.transform.position = t.position;
            _posList.Remove(t); //íœ
            count++;
        }
    }
    /// <summary>
    /// “G‚ªŽ€‚ñ‚¾Žž‚ÉŒÄ‚ñ‚Å‚à‚ç‚¤
    /// </summary>
    /// <param name="t"></param>
    public void Test2(Transform t)
    {
        _posList.Add(t);
    }

    private void Update()
    {
        if (_isPause) return;

        _timer += Time.deltaTime;
        if(_timer >= _timeLimit)
        {
            _timer = 0;
            Test(1);
        }
    }
    void Pause()
    {
        _isPause = true;
    }
    void Resume()
    {
        _isPause = false;
    }
}
