using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class BleedEffect : StatusEffect
{
    public BleedEffect():base("Bleed",false,0) { }

    public override void AtTurnEnd()
    {
        //造成純粹傷害
    }
}
