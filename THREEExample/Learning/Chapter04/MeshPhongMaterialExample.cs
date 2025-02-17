﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Lights;
using THREE.Materials;
using THREE.Math;

namespace THREEExample.Learning.Chapter04
{
    [Example("07.Mesh-Phong-Material", ExampleCategory.LearnThreeJS, "Chapter04")]
    public class MeshPhongMaterialExample : MeshLambertMaterialExample
    {
        public MeshPhongMaterialExample():base()
        {

        }

        public override void BuildMeshMaterial()
        {
            meshMaterial = new MeshPhongMaterial();
            meshMaterial.Color = Color.Hex(0x7777ff);
            meshMaterial.Name = "MeshPhongMaterial";

        }
        public override void AddAmbientLight()
        {
            //base.AddAmbientLight();
        }
        public override void AddSpotLight()
        {
            spotLight = new SpotLight(Color.Hex(0xffffff));
            spotLight.Position.Set(0, 60, 60);
            spotLight.Intensity = 0.6f;
            spotLight.CastShadow = true;
            scene.Add(spotLight);
        }

    }
}
