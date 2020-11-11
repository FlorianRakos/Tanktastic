using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructor : MonoBehaviour
{
    void Awake()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        
        float lifetime = ps.main.duration + ps.main.startLifetime.constant;

        Destroy(gameObject, lifetime);
    }
}
