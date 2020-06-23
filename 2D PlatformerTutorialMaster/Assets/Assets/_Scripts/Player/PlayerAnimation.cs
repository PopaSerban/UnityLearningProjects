using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Animator swordAnimatorAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        swordAnimatorAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float horizontalInput)
    {
        anim.SetFloat("horizontal", Mathf.Abs(horizontalInput));
    }

    public void Jump(bool isJump)
    {
        anim.SetBool("isJump", isJump);
    }
    
    public void Attack()
    {
        anim.SetTrigger("Attack");
        swordAnimatorAnim.SetTrigger("SwordAnimation");
    }
}
