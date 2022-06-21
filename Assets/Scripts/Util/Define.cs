using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Layer
    { 
       Enemy = 3,
       Floor =  6,
       Wall = 7,
       Player = 8,
       Platform = 9,
       AttackCollider = 10,
       //PlayerAttackCollider = 11,
    }


    public enum CreatureState
    {
        Idle,
        Moving,
        Attack,
        Skill,
        Die,
        //InjuredFront,
    }

    public enum Scene
    {
        Unknown,
        Tutorial,
        Stage1,
        Lobby,
        Game,
    }


    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}


