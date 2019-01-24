using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class TreasureChest : MonoBehaviour
{
    Rigidbody2D RBody;


    Collider2D coll;


    Transform itemInside;


    private void Awake()
    {
        RBody = GetComponent<Rigidbody2D>();

        coll= GetComponent<BoxCollider2D>();

        itemInside = transform.GetChild(0);
    }
}
