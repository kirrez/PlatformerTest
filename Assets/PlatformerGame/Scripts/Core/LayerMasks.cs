using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public static class LayerMasks
    {
        public static readonly LayerMask Ground = LayerMask.GetMask("Ground");
        public static readonly LayerMask Slope = LayerMask.GetMask("GroundSlope");
        public static readonly LayerMask OneWay = LayerMask.GetMask("OneWay");
        public static readonly LayerMask PlatformOneWay = LayerMask.GetMask("PlatformOneWay");

        public static readonly LayerMask Solid = Ground + Slope;

        public static readonly LayerMask Walkable = Solid + OneWay + PlatformOneWay;

        public static readonly LayerMask Platforms = PlatformOneWay;
    }
}