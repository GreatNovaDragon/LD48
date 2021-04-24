using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Point(InputAction.CallbackContext context)
    {
        Transform temp = GetComponent<Transform>();

        if (context.ReadValue<Vector2>().x > 0)
        {
            temp.localRotation = Quaternion.Euler(0, 0, 0);
            temp.localPosition = new Vector3(1, 0, 0);

        }
        else if (context.ReadValue<Vector2>().x < 0)
        {
            Debug.Log("hit");

            temp.localRotation = Quaternion.Euler(0, 180f, 0);
            temp.localPosition = new Vector3(-1, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
