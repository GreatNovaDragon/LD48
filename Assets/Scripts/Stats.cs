using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int strength = 1;

    public int speed = 26;
    public int HP;
    public int MaxHP=2;
    public int MaxJump;
    void Start()
    {
        HP=MaxHP;
    }
}
