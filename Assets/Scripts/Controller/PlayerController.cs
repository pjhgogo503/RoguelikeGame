using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStat playerstat;

    public float _jumpPower; //플레이어 점프 파워

    private BoxCollider2D boxCol2D;
    private Rigidbody2D rigid; //플레이어  rigid body
    private Animator animator; //플레이어 애니메이션
    private PossessionController possession; // 플레이어 빙의

    private bool inputIdle = false;
    private bool inputRight = false;
    private bool inputLeft = false;
    private bool inputJump = false;
    public bool ispossession; // 스크립트 내부 빙의 상태 제어
    private string animationState = "AnimationState";

    //플레이어 상태들
    enum States
    {
        Idle = 0,
        Run = 1,
        Attack = 2,
        Skill = 3,
        Die = 4,
    }

    void Init() //플레이어 컴포넌트 연결부분
    {
        _jumpPower = 100;

        boxCol2D = GetComponent<BoxCollider2D>();
        if (PossessionCount.PosCount == 0)
        {
            playerstat = GetComponent<PlayerStat>();
            playerstat.Hp = 100;
        }
        else
        {
            playerstat = GetComponent<PlayerStat>();
            Debug.Log($"first Start : current Hp : {PossessionCount.currentHp} and {playerstat.Hp}");
            playerstat.Hp = PossessionCount.currentHp;
            Debug.Log($"second Start : current Hp : {PossessionCount.currentHp} and {playerstat.Hp}");

        }
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        possession = GetComponent<PossessionController>();
    }

    void Start()
    {
        Init();
        //Input Manager 이용 
        //키 감지
        Managers.Input.KeyAction -= OnKeyBoard; // 이미 작동된 실수 방지
        Managers.Input.KeyAction += OnKeyBoard;
        //키 없는 상태 감지
        Managers.Input.NonKeyAction -= NonKeyBoard;
        Managers.Input.NonKeyAction += NonKeyBoard;
        //마우스 드래그 , 클릭 감지 ( Define 클래스 참고 )
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        float fallSpeed = rigid.velocity.y;
        if(inputIdle)
        {
            inputIdle = false;
            rigid.velocity = new Vector2(0, fallSpeed);
        }
        if (inputRight)
        {
            inputRight = false;
            rigid.velocity = new Vector2(playerstat.MoveSpeed, fallSpeed);
        }
        if(inputLeft)
        {
            inputLeft = false;
            rigid.velocity = new Vector2(-playerstat.MoveSpeed, fallSpeed);
        }
        if (inputJump)
        {
            inputJump = false;
            rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }

    }

    // collider에 닿았을때
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(ispossession)
    //        Debug.Log("OnCollisionEnter2D");

    //    if (collision.gameObject.layer == (int)Define.Layer.Enemy)
    //        Debug.Log($"{collision.gameObject.name}");
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger : {collision.gameObject}");
        if (collision.gameObject.layer == (int)Define.Layer.Enemy)
            collision.GetComponentInParent<Stat>().Hp -= playerstat.Attack;

        if (playerstat.Hp <= 0)
        {
            animator.SetTrigger("isDie");
            Managers.Input.KeyAction -= OnKeyBoard;
            Managers.Input.NonKeyAction -= NonKeyBoard;
            Managers.Input.MouseAction -= OnMouseClicked;
            return;
        }
    }

    //키보드에 뭔가가 들어왔을 때 실행
    void OnKeyBoard()
    {
        RaycastHit2D raycasHit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, LayerMask.GetMask("Floor"));
        Debug.DrawRay(transform.position, new Vector2(0, -0.3f), Color.red);
        if (raycasHit.collider != null)
            animator.SetBool("isJumping", false);
        else animator.SetBool("isJumping", true);

        ispossession = false;
        if (Input.GetKey(KeyCode.A)) // 왼쪽 이동
        {
            inputLeft = true;
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(-1, 1, 1); //왼쪽 바라보는 방향
        }
        if (Input.GetKey(KeyCode.D)) //오른쪽 이동
        {
            inputRight = true;
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(1, 1, 1); //오른쪽 바라보는 방향
        }

        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("isJumping")) //점프
        {
            inputJump = true;
            animator.SetBool("isJumping", true); // 플레이어 점프 상태로 전환
        }
        //빙의 테스트
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ispossession = true;
        }
    }

    //키보드 키 감지가 없는경우 실행
    void NonKeyBoard()
    {
        RaycastHit2D raycasHit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, LayerMask.GetMask("Floor"));
        Debug.DrawRay(transform.position, new Vector2(0, -0.3f), Color.red);
        if (raycasHit.collider != null) animator.SetBool("isJumping", false);
        else animator.SetBool("isJumping", true);

        //플레이어 기본 상태
        inputIdle = true;
        animator.SetInteger(animationState, (int)States.Idle);
    }

    //마우스에 (드래그 , 클릭) 들어왔을 때
    void OnMouseClicked(Define.MouseEvent mouse)
    {
        // 클릭상태이고 현재 플레이어가 Attack 상태가 아닐 때
        if(mouse == Define.MouseEvent.Click)
        {
            //빙의 가능한 상태
            if (ispossession)
            {
                // 반환되는 오브젝트가 적이다?
                if(possession.GetClickedObject().layer == (int)Define.Layer.Enemy)
                {
                    float currentHp = playerstat.Hp;
                    Debug.Log($"current player Hp : { playerstat.Hp}");
                    possession.Possession(possession.GetClickedObject());

                    animator.SetTrigger("isDie");
                    Managers.Input.KeyAction -= OnKeyBoard;
                    Managers.Input.NonKeyAction -= NonKeyBoard;
                    Managers.Input.MouseAction -= OnMouseClicked;

                    gameObject.layer = (int)Define.Layer.Enemy;
                    gameObject.tag = "Untagged";
                    PossessionCount.PosCount++;
                    PossessionCount.currentHp = playerstat.Hp;

                    Destroy(gameObject, 3f);
                }
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    animator.SetTrigger("isAttack");
                    Debug.Log("Attack On!!");
                }
                    
            }
        }
            
    }

    
}
