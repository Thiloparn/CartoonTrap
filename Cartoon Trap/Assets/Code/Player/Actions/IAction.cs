using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction 
{
    protected GameObject onoInstanciated = null;

    public virtual void ExecuteAction(PlayerController player) {}

    protected void InstantiateOnomatopeya(GameObject onomatopeya, PlayerController player)
    {
        if (onoInstanciated != null)
        {
            MonoBehaviour.Destroy(onoInstanciated);
        }

        Vector3 positionOfInstanciation = player.transform.position;
        Vector3 onoOffset = onomatopeya.GetComponent<Onomatopeya>().aparitionPositionOffset * player.LookingAtDirection();
        positionOfInstanciation += onoOffset;

        onoInstanciated = MonoBehaviour.Instantiate<GameObject>(onomatopeya, positionOfInstanciation, Quaternion.identity);
    }
}
