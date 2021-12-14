using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationColorChanger : MonoBehaviour
{
    #region VARIABLES
    [Header("Override de Colores")]
    public AnimatorOverrideController playerOverride_Blue;
    public AnimatorOverrideController playerOverride_BR;
    public AnimatorOverrideController playerOverride_ByN;
    public AnimatorOverrideController playerOverride_FullColor;
    public AnimatorOverrideController playerOverride_GB;
    public AnimatorOverrideController playerOverride_Green;
    public AnimatorOverrideController playerOverride_Red;
    public AnimatorOverrideController playerOverride_RG;

    Animator playerOverride_Animator;
    #endregion

    void Awake()
    {
        playerOverride_Animator = GetComponent<Animator>();
    }

    // Metodo para cambiar el controlador de animacion
    public void Player_ChangeAnimationColor(AnimatorOverrideController playerOverride)
    {
        if (playerOverride_Animator.gameObject.activeSelf) // Comprobamos primero si podemos cambiarlo
        {
            playerOverride_Animator.runtimeAnimatorController = playerOverride; // Se cambia el controlador que se haya introducido
        }
    }
}
