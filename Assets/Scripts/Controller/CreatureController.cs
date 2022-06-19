using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureController : MonoBehaviour
{
    [SerializeField]
    Define.CreatureState _state = Define.CreatureState.Idle;

    [SerializeField]
    protected GameObject _lockTarget;

    public virtual Define.CreatureState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponentInChildren<Animator>();

            switch(_state)
            {
                case Define.CreatureState.Die:
                    anim.CrossFade("Die", 0.1f);
                    break;
                case Define.CreatureState.Idle:
                    anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.CreatureState.Moving:
                    anim.CrossFade("Run", 0.1f);
                    break;
                case Define.CreatureState.Skill:
                    anim.CrossFade("Attack", 0.1f);
                    break;
                case Define.CreatureState.InjuredFront:
                    anim.CrossFade("InjuredFront", 0.1f);
                    break;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (State)
        {
            case Define.CreatureState.Die:
                UpdateDie();
                break;
            case Define.CreatureState.Moving:
                UpdateMoving();
                break;
            case Define.CreatureState.Idle:
                UpdateIdle();
                break;
            case Define.CreatureState.Skill:
                UpdateSkill();
                break;
            case Define.CreatureState.InjuredFront:
                UpdateInjuredFront();
                break;
        }
    }

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateInjuredFront() { }

    protected abstract void Init();
}
