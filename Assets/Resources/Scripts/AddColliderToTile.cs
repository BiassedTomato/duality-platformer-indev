using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[RequireComponent(typeof(TilemapCollider2D), typeof(CompositeCollider2D))]
public class AddColliderToTile : MonoBehaviour
{
   
    private void OnDrawGizmos()
    {
        GetComponent<TilemapCollider2D>().usedByComposite = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        DestroyImmediate(this);
    }
}
