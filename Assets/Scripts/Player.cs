using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{


    private Rigidbody2D body;

    public enum JumpState
    { up, flight, landed }
    public JumpState jumpstate;
    Stats stats;
    Vector2 wanted_velocity;
    public GameObject tilemapGameObject;
    Tilemap tilemap;
    bool jump;

    bool is_grounded;
    IEnumerator jump_couroutine;

    public void Walk(InputAction.CallbackContext context)
    {
        wanted_velocity.x = context.ReadValue<Vector2>().x * stats.speed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (is_grounded)
        {       jump_couroutine = JumpRoutine();
                StartCoroutine(jump_couroutine);
        }
    }
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
        is_grounded = true;
        wanted_velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        switch (jumpstate)
        {
            case JumpState.up:
                wanted_velocity.y = stats.MaxJump;
                is_grounded=false;
                break;
            case JumpState.flight:
                wanted_velocity.y = stats.MaxJump * -1;
                break;
            case JumpState.landed:
                wanted_velocity.y = 0;
                break;
        }
        Debug.Log(wanted_velocity);
        body.velocity = wanted_velocity;
    }
IEnumerator JumpRoutine()
    {
        jumpstate=JumpState.up;


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds((float)0.5);
        jumpstate=JumpState.flight;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && tilemapGameObject == collision.gameObject)
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                TileData e = tilemap.GetTile<TileData>(tilemap.WorldToCell(hitPosition));
                if (e.TileType == TileData.e_TileType.Ground)
                { is_grounded = true; 
                jumpstate=JumpState.landed;}
            }
        }
    }
}

