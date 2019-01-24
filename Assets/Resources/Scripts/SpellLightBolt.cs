using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellLightBolt : IWeapon
{
    PlayerControl player;


    public Queue<Transform> BulletPool;


    public SpellLightBolt(PlayerControl recievedPlayer)
    {
        player = recievedPlayer;

        BulletPool = player.BulletPool;
    }


    public void Use()
    {
        Transform Bullet = BulletPool.Dequeue();

        player.Return(Bullet);

        Bullet.parent = null;
        Bullet.localScale = new Vector3(player.facing, 1, 1);
        Bullet.gameObject.SetActive(true);
    }
}
