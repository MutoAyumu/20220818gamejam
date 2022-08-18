using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemGenerator : MonoBehaviour
{
    public bool _canEnemy = false;

    [SerializeField] Transform[] _spawnPos = default;
    [SerializeField] GameObject[] _testO;

    List<Transform> _posList = new List<Transform>();
    void Start()
    {
        foreach(var t in _spawnPos)
        {
            //List‚É‘S•”’Ç‰Á
            _posList.Add(t);
        }

        Test();
    }

    void Test()
    {
        var count = 0;

        while (_posList.Count != 0)
        {
            var r = Random.Range(0, _posList.Count - 1);

            var t = _posList[r];
            var o = Instantiate(_testO[count]);
            o.transform.position = t.position;
            _posList.Remove(t); //íœ
            count++;

            if(count == _posList.Count)
            {
                break;
            }

            if(_posList.Count == 0)
            {
                break;
            }
        }
    }
    /// <summary>
    /// “G‚ª€‚ñ‚¾‚ÉŒÄ‚ñ‚Å‚à‚ç‚¤
    /// </summary>
    /// <param name="t"></param>
    public void Test2(Transform t)
    {
        _posList.Add(t);
    }
    //void Update()
    //{
    //    if (_canEnemy)
    //    {
    //        StartCoroutine(WomanInterval());
    //    }
    //}
    //IEnumerator WomanInterval()
    //{
    //    yield return new WaitForSeconds(3f);

    //}
    void Set()
    {

    }
}
