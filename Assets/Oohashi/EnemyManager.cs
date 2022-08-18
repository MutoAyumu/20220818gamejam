using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform[] _womanMuzzle;
    [SerializeField] GameObject[] _woman;
    int _count = 0;
    public int _count2 = 0;
    List<int> _random = new List<int>();
    void Start()
    {
        while (true)//無限ループ
        {
            int _randomMuzzle = Random.Range(0, _womanMuzzle.Length);//インスタンス化する位置をランダムにする
            if (_random.Contains(_randomMuzzle))//インスタンス化する位置が被ったら以下を実行する
            {
                _count2++;
                if (_count2 == 100)//ループ限度に達したらループを抜ける(保険)
                {
                    break;
                }
                continue;//もう一度ループ
            }
            GameObject _obj = Instantiate(_woman[_count]);//順番にインスタンス化する
            _obj.transform.position = _womanMuzzle[_randomMuzzle].position;//ランダムで出た位置にオブジェクトをインスタンス化
            _random.Add(_randomMuzzle);
            _count++;
            if (_count >= _womanMuzzle.Length)//すべての位置に配置出来たらループを抜ける
            {
                break;
            }
        }
    }
}
