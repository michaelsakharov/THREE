﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Materials;
using THREE.Math;

namespace THREEExample.Learning.Chapter04
{
    [Example("06.Mesh-Lambert-Material", ExampleCategory.LearnThreeJS, "Chapter04")]
    public class MeshLambertMaterialExample : BasicMeshMaterialExample
    {
        public MeshLambertMaterialExample() : base()
        {

        }
        public override void InitCamera()
        {
            base.InitCamera();
            camera.Position.X = -30;
            camera.Position.Y = 50;
            camera.Position.Z = 40;
            camera.LookAt(new Vector3(10, 0, 0));

        }
        public override void BuildMeshMaterial()
        {
            meshMaterial = new MeshLambertMaterial();
            meshMaterial.Color = Color.Hex(0x7777ff);
            meshMaterial.Name = "MeshLambertMaterial";
        }

    }
}
