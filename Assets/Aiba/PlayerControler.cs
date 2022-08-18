using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _moveSpeed = 5;

    [Header("Rayの長さ")]
    [Tooltip("Rayの長さ")] [SerializeField] float _rayDistance = 3;

    [Header("判定するレイヤー")]
    [Tooltip("判定するレイヤー")] [SerializeField] LayerMask _okLayer;

    [Header("止めていいナンパのタグの名前")]
    [Tooltip("止めていいナンパのタグの名前")] [SerializeField] string _okTatchTagName = "";

    [Header("何もしなくていいナンパのタグの名前")]
    [Tooltip("何もしなくていいナンパのタグの名前")] [SerializeField] string _noTatchTagName = "";

    [Header("アクションを起こすキー")]
    [Tooltip("アクションを起こすキー")] [SerializeField] string inputKey = "Jump";


    Rigidbody2D _rb;
    Animator _anim;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButtonDown(inputKey))
        {
            Judge();
        }
    }



    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 velo = new Vector2(h * _moveSpeed, 0);
        _rb.velocity = velo;
    }

    void Judge()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _rayDistance, _okLayer);

        if (hit)
        {
            //if (hit.collider.gameObject.tag == _okTatchTagName)
            //{

            //}

            //if (hit.collider.gameObject.tag == _noTatchTagName)
            //{

            //}
        }
    }

}
