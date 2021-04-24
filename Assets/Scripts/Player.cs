using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{


    public enum e_jump
    {
        up, peak, down
    }
    public e_jump jumpStatus;
    private Rigidbody2D body;
    Stats stats;
    private IEnumerator jumpRoutine;

    private Transform transform_p;
    float x = 0;
    bool is_jumpin = false;


    public void Walk(InputAction.CallbackContext context)
    {
        x = context.ReadValue<Vector2>().x * stats.speed;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!is_jumpin)
        {
            is_jumpin = true;

            jumpRoutine = JumpEnum();
            StartCoroutine(jumpRoutine);
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        transform_p = GetComponent<Transform>();
        jumpStatus = e_jump.down;
    }

    // Update is called once per frame
    void Update()
    {
        float y = 0;
        switch (jumpStatus)
        {
            case e_jump.up:
                y = stats.speed;
                break;
            case e_jump.down:
                y = -stats.speed;
                break;
            case e_jump.peak:
                y = 0;
                break;
        }

        body.velocity = new Vector2(x, y);

    }
    void FixedUpdate()
    {

    }
    IEnumerator JumpEnum()
    {
        jumpStatus = e_jump.up;
        yield return new WaitForSeconds((float)0.2);
        jumpStatus = e_jump.peak;
        body.gravityScale = 0;
        yield return new WaitForSeconds(0);
        body.gravityScale = 1;
        jumpStatus = e_jump.down;
    }

    void OnColissionStay2D(Collision2D colission)
    {
        Transform collider = colission.collider.GetComponent<Transform>();
        Debug.Log(is_jumpin + " " +collider.position.y );
        if (is_jumpin&&collider.position.y<transform_p.position.x)
        {
            StopCoroutine(jumpRoutine);
            jumpStatus = e_jump.down;

            is_jumpin = false;
        }


    }

}
