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
        while (true)//�������[�v
        {
            int _randomMuzzle = Random.Range(0, _womanMuzzle.Length);//�C���X�^���X������ʒu�������_���ɂ���
            if (_random.Contains(_randomMuzzle))//�C���X�^���X������ʒu���������ȉ������s����
            {
                _count2++;
                if (_count2 == 100)//���[�v���x�ɒB�����烋�[�v�𔲂���(�ی�)
                {
                    break;
                }
                continue;//������x���[�v
            }
            GameObject _obj = Instantiate(_woman[_count]);//���ԂɃC���X�^���X������
            _obj.transform.position = _womanMuzzle[_randomMuzzle].position;//�����_���ŏo���ʒu�ɃI�u�W�F�N�g���C���X�^���X��
            _random.Add(_randomMuzzle);
            _count++;
            if (_count >= _womanMuzzle.Length)//���ׂĂ̈ʒu�ɔz�u�o�����烋�[�v�𔲂���
            {
                break;
            }
        }
    }
}
