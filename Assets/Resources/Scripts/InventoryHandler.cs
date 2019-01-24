using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryHandler : MonoBehaviour
{
    public List<IWeapon> Inventory= new List<IWeapon>();

    private SpellLightBolt Spell1;

    private SpellUnhider Spell2;

    private void Awake()
    {
        Spell1 = new SpellLightBolt(GetComponent<PlayerControl>());

        Spell2 = new SpellUnhider(GetComponent<PlayerControl>());

        Inventory.Add(Spell1);
        Inventory.Add(Spell2);
    }
}