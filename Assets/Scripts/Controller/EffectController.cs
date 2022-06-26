using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public GameObject hit_closeRange;

    public void EffectOn(Transform trans)
    {
        hit_closeRange.transform.position = trans.transform.position + new Vector3(0, 1, -4);
        hit_closeRange.GetComponent<ParticleSystem>().Play();
    }
    
    
}
