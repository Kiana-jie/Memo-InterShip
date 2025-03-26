using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public Transform body;
    public Transform head;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Idle()
    {
        
    }

    public void Walk()
    {

    }

    public void Fly()
    {

    }

    public void DigDown()
    {

    }
    public void DigAround()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
