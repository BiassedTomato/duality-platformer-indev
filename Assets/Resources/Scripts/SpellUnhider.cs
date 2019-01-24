using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellUnhider : IWeapon
{
    PlayerControl player;


    Transform unhider;


    public SpellUnhider(PlayerControl recievedPlayer)
    {
        player = recievedPlayer;
        unhider = player.Unhider;
    }


    public void Use()
    {
            unhider.gameObject.SetActive(true);
            unhider.parent = null;
            Vector3 mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            unhider.transform.position = new Vector3( mousePos.x,mousePos.y,0);
            unhider.localScale = new Vector3(6, 6, 1);

            player.TurnOffUnhider();
    }
}
