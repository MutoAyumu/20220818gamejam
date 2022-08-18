using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("�ړ����x")]
    [Tooltip("�ړ����x")] [SerializeField] float _moveSpeed = 5;

    [Header("Ray�̒���")]
    [Tooltip("Ray�̒���")] [SerializeField] float _rayDistance = 3;

    [Header("���肷�郌�C���[")]
    [Tooltip("���肷�郌�C���[")] [SerializeField] LayerMask _okLayer;

    [Header("�~�߂Ă����i���p�̃^�O�̖��O")]
    [Tooltip("�~�߂Ă����i���p�̃^�O�̖��O")] [SerializeField] string _okTatchTagName = "";

    [Header("�������Ȃ��Ă����i���p�̃^�O�̖��O")]
    [Tooltip("�������Ȃ��Ă����i���p�̃^�O�̖��O")] [SerializeField] string _noTatchTagName = "";

    [Header("�A�N�V�������N�����L�[")]
    [Tooltip("�A�N�V�������N�����L�[")] [SerializeField] string inputKey = "Jump";

    bool _isGameStop = false;

    Rigidbody2D _rb;
    Animator _anim;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
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

    // Update is called once per frame
    void Update()
    {
        if (!_isGameStop)
        {
            Move();
            if (Input.GetButtonDown(inputKey))
            {
                Judge();
            }
        }
    }



    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 velo = new Vector2(h * _moveSpeed, 0);
        _rb.velocity = velo;
       
        if(h!=0)
        {
        transform.localScale = new Vector3(h, 1, 1);
        _anim.SetBool("Run", true);
        }
        else
        {
            _anim.SetBool("Run", false);
        }

    }

    void Judge()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _rayDistance, _okLayer);
       

        if (hit)
        {
            var e = hit.collider.GetComponent<GirlsStatusManager>();
            e.Judge();
             Debug.Log("a");
            //if (hit.collider.gameObject.tag == _okTatchTagName)
            //{

            //}

            //if (hit.collider.gameObject.tag == _noTatchTagName)
            //{

            //}
        }
    }

    void Pause()
    {
        //Pause�ł����鏈��   
        _isGameStop = true;
        if (_anim)
        {
            _anim.speed = 0;
        }

    }

    void Resume()
    {
        //Resume�ł����鏈��   
        _isGameStop = false;
        if (_anim)
        {
            _anim.speed = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        var p = this.transform.position;
        Gizmos.DrawLine(p, new Vector2(p.x, p.y + _rayDistance));
    }
}
