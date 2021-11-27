using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnomaopeyaEffect : MonoBehaviour
{
    public virtual void ExecuteEffect()
    {
        print("Efect");
        Destroy(gameObject);
    }
}
