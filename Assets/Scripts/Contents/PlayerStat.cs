using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public static int _level = 1;
    public static float _hp = 500;
    public static float _maxhp = 500;
    public static int _attack = 10;
    public static float _attackSpeed = 2; 
    public static float _moveSpeed = 5;
    public static int _exp = 0;
    public static int _gold = 0;
    public static float _jumpPower = 100;

    public static bool test = false;

    public static int Level { get { return _level; } set { _level = value; } }
    public static float Hp { get { return _hp; } set { _hp = value; } }
    public static float MaxHp { get { return _maxhp; } set { _maxhp = value; } }
    public static int Attack { get { return _attack; } set { _attack = value; } }
    public static float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public static float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public static int Exp { get { return _exp; } set { _exp = value; } }
    public static int Gold { get { return _gold; } set { _gold = value; } }
    public static float JumpPower { get { return _jumpPower; } set { _jumpPower = value; } }

    public static bool Test { get { return test; } set { test = value; } }


}
