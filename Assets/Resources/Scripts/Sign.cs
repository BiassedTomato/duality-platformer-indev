using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Sign : MonoBehaviour
{
    public string[] MessageGroup = new string[] { "This is a test message for ya.", "Can ya see it?", "Nah, dun' care if you do." };


    public string TerrainTag="Sign";
    public string Message;

   public string[] OnUse()
    {
        return MessageGroup;
    }

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.tag = "Terrain";
    }

}
