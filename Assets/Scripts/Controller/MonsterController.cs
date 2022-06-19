using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : CreatureController
{
    Stat _stat;
    PlayerStat playerstat;
    Rigidbody2D _rigid;

    [SerializeField]
    float _scanRange = 5;
    [SerializeField]
    float _attackRange = 1.7f;
    [SerializeField]
    float distance;
    [SerializeField]
    float height;

    Vector2 frontVec;
    RaycastHit2D bottomcheck, frontcheck;

    int testcount = 0;

    private Animator anim;

    //Idle 움직임 결정
    int movementFlag;
    bool AI = false;

    private void Awake()
    {
        Invoke("Think", 2);
    }

    void Think()
    {
        testcount++;
        //Debug.Log($"invoke counting : {testcount}");
        movementFlag = Random.Range(-1, 2);
        Invoke("Think", 2);
    }

    protected override void Init()
    {
        _lockTarget = GameObject.FindWithTag("Player");
        _stat = gameObject.GetComponent<Stat>();
        _rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerstat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    //크리쳐와 플레이어 간의 X, Y 값의 절대값 계산
    void XYcheck()
    {
        distance = Mathf.Abs((_lockTarget.transform.position - transform.position).x);
        height = Mathf.Abs((_lockTarget.transform.position - transform.position).y);
    }

    // 움직임 관리 벡터 계산, 자주 사용하는 레이캐스트 계산
    void VectorAndRay()
    {
        frontVec = new Vector2(_rigid.position.x + movementFlag, _rigid.position.y);
        bottomcheck = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor"));
        frontcheck = Physics2D.Raycast(frontVec + new Vector2(0, 1), movementFlag == 1 ? Vector3.right : Vector3.left, 1, LayerMask.GetMask("Floor"));


    }

    protected override void UpdateIdle()
    {
        XYcheck(); // 매 프레임마다 거리 계산
        VectorAndRay();

        if (distance <= _scanRange && height < 1) // X 거리가 _scanRange이내이고 높이차이가 1 미만일때
        {
            Debug.Log("Find Player");
            if (distance < _attackRange)
            {
                State = Define.CreatureState.Moving;
                return;
            }
            Debug.DrawRay(frontVec, Vector3.down, Color.green);
            if (bottomcheck.collider == null)
            {
                CancelInvoke();
                return;
            }
            // + 락타겟 하는 방법 고민
            AI = false;
            State = Define.CreatureState.Moving;
            return;
        }
        else { AI = true; if (movementFlag != 0) State = Define.CreatureState.Moving; }

        if(_stat.Hp == 0)
        {
            State = Define.CreatureState.Die;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        XYcheck();
        VectorAndRay();

        if (AI)
        {
            if (distance <= _scanRange && height < 1)
            {
                Debug.Log("Find Player");
                // + 락타겟 하는 방법 고민
                AI = false;
                State = Define.CreatureState.Moving;
                return;
            }
            MoveFlag(movementFlag);

            Debug.DrawRay(frontVec, Vector3.down, Color.red);
            Debug.DrawRay(frontVec + new Vector2(0, 1), movementFlag == 1 ? Vector3.right : Vector3.left, Color.red);

            if (bottomcheck.collider == null || frontcheck.collider != null)
            {
                movementFlag = movementFlag * (-1);
                CancelInvoke();
                Invoke("Think", 2);
                return;
            }
        }
        else
        {
            CancelInvoke();
            if (transform.position.x < _lockTarget.transform.position.x) movementFlag = 1;
            else movementFlag = -1;

            MoveFlag(movementFlag);
            Debug.DrawRay(frontVec, Vector3.down, Color.green);

            if (distance <= _attackRange && height < 1)
            {
                State = Define.CreatureState.Skill;
                return;
            }

            if (bottomcheck.collider == null || frontcheck.collider != null || distance > _scanRange)
            {
                State = Define.CreatureState.Idle;
                Invoke("Think", 2);
                return;
            }
        }
        if (_stat.Hp == 0)
        {
            State = Define.CreatureState.Die;
            return;
        }
    }

    protected override void UpdateSkill()
    {
        XYcheck();

        // 공격범위보다 distance가 멀어짐과 동시에 Attack 애니메이션이 1번이상 실행 된 상태
        if (_attackRange < distance && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            State = Define.CreatureState.Moving;
            return;
        }

        if (_stat.Hp == 0)
        {
            State = Define.CreatureState.Die;
            return;
        }
    }

    protected override void UpdateInjuredFront() 
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if(distance < _attackRange)
            {
                State = Define.CreatureState.Skill;
                return;
            }
            else
            {
                State = Define.CreatureState.Moving;
                return;
            }
        }
    }

    protected override void UpdateDie()
    {
            Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Monster hit! : {collision.name}");
        if(State != Define.CreatureState.Die)
        {
            if(State == Define.CreatureState.Skill)
            {
                Debug.Log("Monster Attack On");
                if (collision.gameObject.layer == (int)Define.Layer.Player)
                    playerstat.Hp -= _stat.Attack;
            }
            else
            {
                if (collision.gameObject.layer == (int)Define.Layer.Player)
                    State = Define.CreatureState.InjuredFront;
            }
        } 
    }

    void MoveFlag(int movementFlag)
    {
        switch (movementFlag)
        {
            case -1:
                transform.localScale = new Vector3(-1, 1, 1); //왼쪽 바라보는 방향
                transform.Translate(Vector3.left * Time.deltaTime * (_stat.MoveSpeed));  //방향 * 속도
                break;
            case 1:
                transform.localScale = new Vector3(1, 1, 1); //오른쪽 바라보는 방향
                transform.Translate(Vector3.right * Time.deltaTime * (_stat.MoveSpeed));  //방향 * 속도
                break;
            default:
                State = Define.CreatureState.Idle;
                break;
        }
    }

}
