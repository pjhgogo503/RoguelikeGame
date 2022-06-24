using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public Transform WeaponPosition;
    public GameObject AttackCollider;
    public GameObject Timer;
    public bool equipWeapon = false;

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

    public void PossessionTimerOn()
    {
        //Timer.transform.position = /*Camera.main.WorldToScreenPoint(*/gameObject.transform.parent.gameObject.transform.position + Vector3.up;//);
        Timer.SetActive(true);
    }
}
