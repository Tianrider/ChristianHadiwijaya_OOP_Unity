using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private Animator animator;

    void Awake () 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.move();
    }

    void LateUpdate() 
    {
        animator.SetBool("IsMoving", playerMovement.isMoving());
    }
}