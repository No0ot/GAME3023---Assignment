using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    //Make A Game Like Pokemon in Unity | #6 - Creating Moves For Pokemon https://youtu.be/h3EZ9G0Gtx0
    private AbilityBase base_;

    public Ability(AbilityBase ability_base)
    {
        base_ = ability_base;
    }

    public AbilityBase GetBase()
    {
        return base_;
    }
}
