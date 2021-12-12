using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnomatopeyaCollider), typeof(OnomaopeyaEffect))]
public class Onomatopeya : MonoBehaviour
{
    private bool active;
    private bool throwed;
    public int damage;
    public Vector2 aparitionPositionOffset;
    protected OnomaopeyaEffect effect;
    public GameObject onoAreaEffect;
    public GameObject negativeOnoAreaEffect;

    public bool Active { get => active; set => active = value; }
    public bool Throwed { get => throwed;}

    protected virtual void Awake()
    {
        Active = false;
        throwed = false;
        effect = GetComponent<OnomaopeyaEffect>();
    }

    private void FixedUpdate()
    {
        if(Active)
        {
            effect.ExecuteEffect();
        }
    }

    public void DestroyOnomatopeya() //Metodo que podrian heredar los hijos en caso de que al destruirse las onomatopeyas hagan algo
    {
        Destroy(gameObject);
    }

    public void EnterPocket()
    {
        gameObject.SetActive(false);
    }

    public virtual void ExitPocket(float direction, Vector3 position)
    {
        ThrowedOno throwedOno = gameObject.AddComponent<ThrowedOno>();
        throwedOno.direction = direction;
        throwed = true;
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void TransformToBadOno()
    {
        Destroy(GetComponent<OnomatopeyaCollider>());
        gameObject.AddComponent<NegativeOnomatopeyaCollider>();
        onoAreaEffect = negativeOnoAreaEffect;
    }

}
