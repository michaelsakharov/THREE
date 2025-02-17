﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Core;
using THREE.Math;
namespace THREE.Lights
{
    public class HemisphereLight : Light
    {
        public HemisphereLight(Color skyColor, Color groundColor, float? itensity = null)
            : base(skyColor, itensity)
        {
            this.CastShadow = false;

            this.Position.Copy(Object3D.DefaultUp);

            this.UpdateMatrix();

            this.GroundColor = groundColor;

            this.type = "HemisphereLight";
        }

        protected HemisphereLight(HemisphereLight other) : base(other)
        {
            this.type = "HemisphereLight";
            this.GroundColor = other.GroundColor;
        }
    }
}
