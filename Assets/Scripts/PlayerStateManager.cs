using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public int playerEquippedState;
    SpriteRenderer sprite;
    Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        playerEquippedState = 0;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerEquippedState = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerEquippedState = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerEquippedState = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerEquippedState = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerEquippedState = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerEquippedState = 5;
        }

        if(playerEquippedState == 0)
        {
            animator.SetInteger("itemHeld", 0);
        }
        if (playerEquippedState == 1)
        {
            animator.SetInteger("itemHeld", 1);
        }
        if (playerEquippedState == 2)
        {
            animator.SetInteger("itemHeld", 2);
        }
        if (playerEquippedState == 3)
        {
            animator.SetInteger("itemHeld", 3);
        }
        if (playerEquippedState == 4)
        {
            animator.SetInteger("itemHeld", 4);
        }
        if (playerEquippedState == 5)
        {
            animator.SetInteger("itemHeld", 5);
        }
    }
}
