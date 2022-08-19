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

    [Header("アクションを起こすキー")]
    [Tooltip("アクションを起こすキー")] [SerializeField] string inputKey = "Jump";

    [Header("SEの配列")]
    [Tooltip("SEの配列")] [SerializeField] AudioClip[] _clips;

    [Header("2つ目のAudioSource")]
    [Tooltip("2つ目のAudioSource")] [SerializeField] AudioSource _audio2;

    [Header("3つ目のAudioSource")]
    [Tooltip("3つ目のAudioSource")] [SerializeField] AudioSource _audio3;

    bool _isGameStop = false;
    bool _isHit = false;

    Rigidbody2D _rb;
    Animator _anim;
    AudioSource _audio;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _audio = gameObject.GetComponent<AudioSource>();
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
        if (!_isGameStop && !GameManager.Instance._isGameStart)
        {
            Move();
            if (Input.GetButtonDown(inputKey) && !_isHit)
            {
                int _randomAttack = Random.Range(1, 4);
                _anim.Play($"attack{_randomAttack}");
                int _randomAudio = Random.Range(0, _clips.Length);
                _audio.PlayOneShot(_clips[_randomAudio]);
                _audio3.Play();
                _isHit = true;
                StartCoroutine(HitInterval());
                Judge();
            }
        }
    }



    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector2 velo = new Vector2(h * _moveSpeed, _rb.velocity.y);
        _rb.velocity = velo;
        _anim.SetFloat("Run", h);
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            _audio2.Play();
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _audio2.Stop();
        }
    }

    void Judge()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, _rayDistance, _okLayer);
       

        if (hit)
        {

            var e = hit.collider.GetComponent<GirlsStatusManager>();
            e.Judge();
            
            //if (hit.collider.gameObject.tag == _okTatchTagName)
            //{

            //}

            //if (hit.collider.gameObject.tag == _noTatchTagName)
            //{

            //}
        }
    }
    IEnumerator HitInterval()
    {
        yield return new WaitForSeconds(1f);
        _isHit = false;
    }

    void Pause()
    {
        //Pauseでさせる処理   
        _isGameStop = true;
        if (_anim)
        {
            _anim.speed = 0;
        }
    }

    void Resume()
    {
        //Resumeでさせる処理   
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
