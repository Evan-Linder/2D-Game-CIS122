using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = GetComponent<PlayerControls>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    void Start()
    {
        playerControls.Combat.Attack1.started += _ => Attack();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }
}

