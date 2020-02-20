using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttackBehaviour : StateMachineBehaviour
{
    private GameObject Player;
    private GameObject NPC;

    float timeBtwBatAtt = 2f;
    float timeTillNextAttack = 4f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        NPC = animator.gameObject;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.transform.LookAt(new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z));

        if (timeTillNextAttack <= 0) //Check to see if the time has reached 0 or less than
        {
            int rand = Random.Range(0, 3); //Pick a random attack

            if (rand == 0) //Shoot a Fireball
            {
                NPC.GetComponent<NecromancerSc>().shootFireball(Player);
                timeTillNextAttack = Random.Range(2f, 5f); //Randomize the time until the Necromancers next attack
            }
            else if (rand == 1) //Summon Zombies
            {
                NPC.GetComponent<NecromancerSc>().summonZombies(Player);
                timeTillNextAttack = Random.Range(2f, 5f); //Randomize the time until the Necromancers next attack
            }
            else if (rand == 2) //Shoot Homing Fireballs
            {
                NPC.GetComponent<NecromancerSc>().shootHomingFireballs(Player);
                timeTillNextAttack = Random.Range(2f, 5f); //Randomize the time until the Necromancers next attack
            }
        }
        else
        {
            timeTillNextAttack -= Time.deltaTime; //If the time remaining is still above 0, decrease it
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
