using System.Collections;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    
    [SerializeField] private Transform weaponCollider; // Reference to the collider
    public bool isAttacking { get; private set; }
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;

    public bool IsAttacking { get { return isAttacking; } }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>(); // Reference the Animator component
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        MouseFollowWithOffset();

        // Input handling
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Assuming left mouse button for attack
        {
            ExecuteAttack();
        }
    }

    public void ExecuteAttack()
    {
        isAttacking = true; // Set attacking bool to true
        myAnimator.SetTrigger("basicAttack"); // Trigger the attack animation
        weaponCollider.gameObject.SetActive(true); // Enable the collider
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(0.5f); // Duration of the swing (adjust as needed)
        weaponCollider.gameObject.SetActive(false); // Disable the collider
        isAttacking = false; // Reset attacking bool
        yield return new WaitForSeconds(0.6f); // Duration of the swing (adjust as needed)
        myAnimator.SetBool("basicAttack", false); // Reset the animator parameter

    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position); // Convert player's position to screen coords

        if (mousePos.x < playerScreenPoint.x)
        {
            // Check if mouse is to the left
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            // Check if mouse is to the right 
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}





