using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected float _maxhp;

    [SerializeField]
    protected int _attck;
    [SerializeField]
    protected float _moveSpeed;

    [SerializeField]
    protected bool _possession;


    public int Level { get { return _level; } set { _level = value; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public int Attack { get { return _attck; } set { _attck = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public bool Possession { get { return _possession; } set { _possession = value; } }

    private void Start()
    {
        _possession = false;

        //_level = 1;
        //_hp = 100;
        //_maxhp = 100;
        //_attck = 10;
        //_moveSpeed = 2;
    }
}
