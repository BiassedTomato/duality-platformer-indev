using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    Transform LightField;


    private void Awake()
    {
        LightField = transform.GetChild(0);

        StartCoroutine("Pulse");
    }


    private void OnDrawGizmos()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }


    IEnumerator Pulse()
    {
        for (; ; )
        {
            LightField.localScale = new Vector2(Random.Range(7f, 10f), Random.Range(8f, 16f));

            yield return new WaitForSeconds(0.05f);

            yield return 0f;
        }
    }
}
