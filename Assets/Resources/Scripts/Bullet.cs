using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    Rigidbody2D RBody;


    Collider2D Coll;


    Transform LightField;


    IEnumerator Pulse()
    {
        for (; ; )
        {
            LightField.localScale = new Vector2(Random.Range(4f, 8f), Random.Range(2f, 6f));

            yield return new WaitForSeconds(0.03f);

            yield return 0f;
        }
    }


    void GetImportantComponents()
    {
        LightField = transform.GetChild(0);

        StartCoroutine("Pulse");

        RBody = GetComponent<Rigidbody2D>();
        RBody.gravityScale = 0;

        Coll = GetComponent<Collider2D>();
        Coll.isTrigger = true;
    }


    IEnumerator GetDirection()
    {
        yield return new WaitForEndOfFrame();

        RBody.velocity = Vector2.right * 7.5f * transform.localScale.x;
    }


    private void OnEnable()
    {
        GetImportantComponents();

        StartCoroutine("GetDirection");
    }

    private void OnDisable()
    {
        StopCoroutine("GetDirection");
    }
}
