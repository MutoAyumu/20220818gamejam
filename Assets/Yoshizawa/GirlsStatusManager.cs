using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlsStatusManager : MonoBehaviour
{
    [SerializeField]JudgeType state;
    [SerializeField] int _score = 0;

    public void Judge()
    {
        if(state == JudgeType.good)
        {
            GameManager.Instance.AddScore(_score);
        }
        else if(state == JudgeType.bad)
        {
            GameManager.Instance.DecreaseScore(_score);
        }
    }
       

    enum JudgeType
    {
        /// <summary>ナンパがうれしいとき</summary>
        good,
        /// <summary>ナンパがいやなとき </summary>
        bad,
    }
}
