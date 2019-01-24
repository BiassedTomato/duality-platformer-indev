using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lantern : MonoBehaviour
{
    Rigidbody2D RBody;
    Transform LightField;
    // Use this for initialization
    void Awake()
    {
        RBody = GetComponent<Rigidbody2D>();
        LightField = transform.GetChild(0);
        StartCoroutine("Pulse");
    }

    private void OnMouseUp()
    {
        RBody.gravityScale = 5;
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0);

        RBody.velocity = (pos - transform.position)*2;
    }

    private void OnMouseDown()
    {
        RBody.gravityScale = 0;
    }

    IEnumerator Pulse()
    {
        for (; ; )
        {
            LightField.localScale = new Vector2(Mathf.Lerp(LightField.localScale.x, Random.Range(0.7f,1.2f),0.5f), Mathf.Lerp(LightField.localScale.y, Random.Range(0.7f, 1.2f), 0.5f));

            yield return new WaitForSeconds(0.08f);

            yield return 0f;
        }
    }
}
