using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if a key is pressed down or left arrow key is down 
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetBool("Turn Left" , true);
            _anim.SetBool("Turn Right", false);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _anim.SetBool("Turn Left", false);
            _anim.SetBool("Turn Right", false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetBool("Trun Right", true);
            _anim.SetBool("Turn Left", false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _anim.SetBool("Turn Right", false);
            _anim.SetBool("Turn Left", false);
        }
    }
}
