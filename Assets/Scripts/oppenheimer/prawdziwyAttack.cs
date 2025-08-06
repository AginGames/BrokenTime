using UnityEngine;
using UnityEngine.UIElements;

public class prawdziwyAttack : StateMachineBehaviour
{
    public Transform firePoint;
    public float shootDelay = 0.5f;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    public float bulletForce = 2f;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("ATTACK");
        firePoint = animator.GetComponent<Transform>();

        Shoot(45);
        Shoot(90);
        Shoot(135);
        Shoot(180);
        Shoot(225);
        Shoot(270);
        Shoot(315);
        Shoot(0);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("ATTACKEND");
    }
    public void Shoot(float angle)
    {
        Transform dfirePoint = firePoint;
        dfirePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        GameObject bullet = Instantiate(bulletPrefab, dfirePoint.position, dfirePoint.rotation);
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dfirePoint.up * bulletForce, ForceMode2D.Impulse);


    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
