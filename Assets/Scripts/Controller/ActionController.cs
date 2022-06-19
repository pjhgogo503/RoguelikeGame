using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Transform WeaponPosition;

    public GameObject AttackCollider;

    public bool equipWeapon;

    public void AttackColliderOnOff()
    {
        Debug.Log("anim event on");
        if (equipWeapon) equipWeapon = false;
        else equipWeapon = true;

        if (equipWeapon)
        {
            AttackCollider.SetActive(true);
        }
        else AttackCollider.SetActive(false);
    }

}
