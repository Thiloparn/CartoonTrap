using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction 
{
    private GameObject onoInstanciated = null;
    protected Vector3 positionOfInstanciation = Vector3.zero;

    public virtual void ExecuteAction(PlayerController player) {}

    protected void InstantiateOnomatopeya(GameObject onomatopeya)
    {
        if (onoInstanciated != null)
        {
            MonoBehaviour.Destroy(onoInstanciated);
        }

        onoInstanciated = MonoBehaviour.Instantiate<GameObject>(onomatopeya, positionOfInstanciation, Quaternion.identity);
    }
}
