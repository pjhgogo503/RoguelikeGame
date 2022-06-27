using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    string parentName;
    GameObject projectile;

    private void Start()
    {
        parentName = gameObject.transform.parent.tag;
    }

    public void Shoot()
    {
        switch (parentName) 
        {
            case "Player":
                projectile = Managers.Resource.Instantiate($"Creature/Arrow");

                if (gameObject.transform.parent.transform.localScale.x >= 0)
                {
                    projectile.transform.localScale = new Vector3(1, 1, 1);
                    projectile.transform.position = gameObject.transform.position + new Vector3(1, 1.4f, 0);
                }
                else
                {
                    projectile.transform.localScale = new Vector3(-1, 1, 1);
                    projectile.transform.position = gameObject.transform.position + new Vector3(-1, 1.4f, 0);
                }
                break;
            case "Landing_Long":
                projectile = Managers.Resource.Instantiate($"Creature/Arrow");

                if (gameObject.transform.parent.transform.localScale.x >= 0)
                {
                    projectile.transform.localScale = new Vector3(1, 1, 1);
                    projectile.transform.position = gameObject.transform.parent.transform.position + new Vector3(1, 1.4f, 0);
                }
                else
                {
                    projectile.transform.localScale = new Vector3(-1, 1, 1);
                    projectile.transform.position = gameObject.transform.parent.transform.position + new Vector3(-1, 1.4f, 0);
                }
                break;
            
        }
    }
}
