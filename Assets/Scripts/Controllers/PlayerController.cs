using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Moving,
    }
    float idle_run_ratio = 0f;

    PlayerState _playerState = PlayerState.Idle;

    [SerializeField]
    float _speed = 5f;


    float joyX
    {
        get
        {
            return Managers.Input.joyX;
        }
    }
    float joyY
    {
        get
        {
            return Managers.Input.joyY;
        }
    }

    Rigidbody2D rigid;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Managers.Input.JoystickAction -= OnAnyKey;
        Managers.Input.JoystickAction += OnAnyKey;
    }


    void Update()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                idle_run_ratio = Mathf.Lerp(idle_run_ratio, 0, 10.0f * Time.deltaTime);
                anim.SetFloat("idle_run_ratio", idle_run_ratio);
                anim.Play("IDLE_RUN");
                break;

            case PlayerState.Moving:
                idle_run_ratio = Mathf.Lerp(idle_run_ratio, 1, 10.0f * Time.deltaTime);
                anim.SetFloat("idle_run_ratio", idle_run_ratio);
                anim.Play("IDLE_RUN");
                break;
        }
    }

    void OnAnyKey()
    {
        Move();
    }

    void Move()
    {
        if (joyX != 0 && joyY != 0)
        {
            Vector2 inputDir = new Vector2(joyX, joyY) * _speed * Time.deltaTime;
            float rotationDir = inputDir.x > 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotationDir, 0));

            rigid.MovePosition(rigid.position + inputDir);
            _playerState = PlayerState.Moving;
        }
        else
        {
            _playerState = PlayerState.Idle;
        }
    }
}
