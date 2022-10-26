using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public interface IPlayerConfig
    {
        float HorizontalSpeed { get; }
        float CrouchSpeed { get; }
        float PushDownForce { get; }
        float JumpForce { get; }
        float RollDownForce { get; }
    }
}