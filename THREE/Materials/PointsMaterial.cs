﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Math;
namespace THREE.Materials
{
    public class PointsMaterial : Material
    {
        public float Size = 1;

        public PointsMaterial() : base()
        {
            this.type = "PointsMaterial";

            this.Color = new Color().SetHex(0xffffff);

            this.Map = null;

            this.AlphaMap = null;

            this.Size = 1;

            this.SizeAttenuation = true;

            this.MorphTargets = false;
        }
    }
}
