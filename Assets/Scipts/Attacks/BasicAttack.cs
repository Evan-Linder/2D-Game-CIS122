using System.Collections;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private bool isAttacking = false; // Flag to check if player is attacking
    private Animator myAnimator;

    public bool IsAttacking { get { return isAttacking; } }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>(); // reference the Animator component
    }

    public void ExecuteAttack()
    {
        isAttacking = true; // set attacking bool true
        myAnimator.SetBool("basicAttack", true); // set animator parameter to true
        myAnimator.SetTrigger("basicAttack"); // trigger the attack animation

        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(1.2f); // duration of attack animation

        isAttacking = false; //reset attacking bool
        myAnimator.SetBool("basicAttack", false); // reset animator parameter
    }
}

