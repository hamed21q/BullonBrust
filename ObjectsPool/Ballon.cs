using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : FallingItem
{
    [SerializeField] float acceleration;
    public override void Update()
    {
        base.Update();
        fallSpeed += Time.deltaTime * acceleration;
    }
}
