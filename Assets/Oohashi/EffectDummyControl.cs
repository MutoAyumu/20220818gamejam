using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDummyControl : MonoBehaviour
{

    [SerializeField] GameObject _effect;
    [SerializeField] Transform _effectPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        if(h < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if(h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void Effect()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            var go = Instantiate(_effect);
            go.transform.localScale= new Vector3(-1, 1, 1);
            go.transform.position = _effectPos.transform.position;
        }
        else if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            var go = Instantiate(_effect);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.position = _effectPos.transform.position;
        }
    }

}
