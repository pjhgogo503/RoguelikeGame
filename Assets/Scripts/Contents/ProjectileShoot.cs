using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField]
    float _speed = 1f;
    [SerializeField]
    int _attack = 10;
    float distance = 0.2f;
    float angle;
    RaycastHit2D hit;
    RaycastHit2D ray;
    GameObject Player;
    Vector3 PlayerShotdir = Vector3.zero;
    static string Who;

    private void Start()
    {
        if (gameObject.transform.localScale.x >= 0) hit = Physics2D.Raycast(transform.position, Vector3.left, 2f, LayerMask.GetMask("Player", "Enemy"));
        else hit = Physics2D.Raycast(transform.position, Vector3.right, 2f, LayerMask.GetMask("Player", "Enemy"));

        switch(hit.collider.gameObject.layer)
        {
            case (int)Define.Layer.Player:
                Player = GameObject.FindGameObjectWithTag("Player");
                PlayerShotdir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position;
                angle = Mathf.Atan2(PlayerShotdir.y, PlayerShotdir.x) * Mathf.Rad2Deg;
                Debug.Log($"{ angle} µµ");
                if (this.transform.localScale == new Vector3(1, 1, 1)) transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                else transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

                PlayerShotdir = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position).normalized;
                //if (this.transform.localScale == new Vector3(-1, 1, 1))
                //    transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                //else transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //angle = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) * Mathf.Rad2Deg;
                //if (this.transform.localScale == new Vector3(1, 1, 1))
                //{
                //    if (angle >= 0)
                //    {
                //        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y >= 0)
                //            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //        else transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                //    }
                //    else
                //    {
                //        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y <= 0)
                //            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //        else transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                //    }
                //}
                //else
                //{
                //    if (angle >= 0)
                //    {
                //        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y >= 0)
                //            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                //        else transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //    }
                //    else
                //    {
                //        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y >= 0)
                //            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //        else transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                //    }
                //}
                Who = "Player"; break;
            case (int)Define.Layer.Enemy:
                Who = "Enemy"; break;
        }
        Invoke("DestroyShot", 2);
    }

    void Update()
    {
        switch (Who)
        {
            case "Player":
                PlayerShot(); break;
            case "Enemy":
                Shot(); break;
        }
    }

    void PlayerShot()
    {
        gameObject.transform.Translate(PlayerShotdir * _speed * Time.deltaTime);
        ray = Physics2D.Raycast(transform.position, PlayerShotdir, distance, LayerMask.GetMask("Enemy", "Floor"));

        if (ray.collider == null) Invoke("DestroyShot", 2);
        else
        {
            if (ray.collider.gameObject.layer == (int)Define.Layer.Enemy)
            {
                if (ray.collider.GetComponent<Stat>().Hp > 0)
                    ray.collider.GetComponent<Stat>().Hp -= _attack;
                Destroy(gameObject);
            }
            if (ray.collider.gameObject.layer == (int)Define.Layer.Floor)
                Destroy(gameObject);
        }
    }

    void Shot()
    {
        if (gameObject.transform.localScale.x >= 0)
        {
            gameObject.transform.Translate(new Vector3(1, 0, 0) * _speed * Time.deltaTime);
            ray = Physics2D.Raycast(transform.position, Vector3.right, distance, LayerMask.GetMask("Player", "Floor"));
        }
        else
        {
            gameObject.transform.Translate(new Vector3(-1, 0, 0) * _speed * Time.deltaTime);
            ray = Physics2D.Raycast(transform.position, Vector3.left, distance, LayerMask.GetMask("Player", "Floor"));
        }
        
        if (ray.collider == null) Invoke("DestroyShot", 2);
        else
        {
            if (ray.collider.gameObject.layer == (int)Define.Layer.Player)
            {
                if (PlayerStat.Hp > 0)
                    PlayerStat.Hp -= _attack;
                Destroy(gameObject);
            }
            if (ray.collider.gameObject.layer == (int)Define.Layer.Floor)
                Destroy(gameObject);
        }
    }

    public void DestroyShot()
    {
        Destroy(gameObject);
    }
        
}
