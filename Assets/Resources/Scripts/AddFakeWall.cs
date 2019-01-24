using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[RequireComponent(typeof(TilemapCollider2D), typeof(CompositeCollider2D))]
public class AddFakeWall : MonoBehaviour
{
    TilemapCollider2D tmapcoll;
    private void Awake()
    {
        tmapcoll = GetComponent<TilemapCollider2D>();
        tmapcoll.usedByComposite = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Light")
            tmapcoll.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Light")
            tmapcoll.enabled = false;
    }


}
