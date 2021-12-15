using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnomaopeyaEffect : MonoBehaviour
{
    protected Onomatopeya ono;

    private void Awake()
    {
        ono = GetComponent<Onomatopeya>();
    }

    public virtual void ExecuteEffect()
    {
        if (ono.onoAreaEffect != null)
        {
            InstantiateAreaEffect();
        }
        ono.DestroyOnomatopeya();
    }

    public virtual void InstantiateAreaEffect()
    {
    }
}
