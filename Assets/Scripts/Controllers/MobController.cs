using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    enum MobState
    {
        Idle,
        Attack,
        Die,
    }

    bool canAttack = true;
    bool attacking = false;

    MobState _mobState = MobState.Idle;

    [SerializeField]
    float _speed = 3f;
    [SerializeField]
    float _attackSpeed = 3f;
    IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(_attackSpeed);
        canAttack = true;
    }

    IEnumerator AttackSpeed()
    {
        attacking = true;
        yield return new WaitForSecondsRealtime(1f);
        attacking = false;
    }


    bool nearByPlayer = false;


    Rigidbody2D rigid;
    Animator anim;
    Transform target;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player").transform.GetChild(0).GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            nearByPlayer = true;
            _mobState = MobState.Attack;
        } 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            nearByPlayer = false;
            _mobState = MobState.Idle;
        }
    }

    void Update()
    {
        switch (_mobState)
        {
            case MobState.Idle:

                if(!attacking)anim.Play("IDLE");
                break;
        }
        switch (_mobState)
        {
            case MobState.Attack:
                if (canAttack)
                {
                    anim.Play("ATTACK");
                    StartCoroutine(AttackSpeed());
                    StartCoroutine(AttackDelay());
                    canAttack = false;
                }
                else _mobState = MobState.Idle;
                break;
        }
        switch (_mobState)
        {
            case MobState.Die:
                anim.Play("DIE");
                break;
        }
        MoveAttact();
        
    }


    void MoveAttact()
    {
        Vector2 dis = (target.position - transform.position);
        float rotationDir = dis.x > 0 ? 0 : 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotationDir, 0));
        if (nearByPlayer && canAttack) _mobState = MobState.Attack;
        else
        {
            if(!attacking) _mobState = MobState.Idle;
            if (dis.magnitude < 0.7f) return;
            rigid.MovePosition(rigid.position + dis.normalized * _speed * Time.deltaTime);
        }
    }
}
