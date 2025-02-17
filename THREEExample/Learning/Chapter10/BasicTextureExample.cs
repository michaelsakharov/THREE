﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Geometries;
using THREE.Lights;
using THREE.Loaders;
using THREE.Math;
using THREEExample.Learning.Chapter10;
using THREEExample.Learning.Utils;
using THREEExample.ThreeImGui;
using THREE.Textures;
using THREE;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace THREEExample.Learning.Chapter09
{
    [Example("01-basic-texture", ExampleCategory.LearnThreeJS, "Chapter10")]
    public class BasicTextureExample : TemplateExample
    {
       
        public BasicTextureExample() : base()
        {

        }

        public override void Load(GLControl control)
        {
            Debug.Assert(null != control);

            glControl = control;
            this.renderer = new THREE.Renderers.GLRenderer();

            this.renderer.Context = control.Context;
            this.renderer.Width = control.Width;
            this.renderer.Height = control.Height;

            this.renderer.Init();

            stopWatch.Start();

            InitRenderer();

            InitCamera();

            InitCameraController();

            imguiManager = new ImGuiManager(this.glControl);

            var groundPlane = DemoUtils.AddLargeGroundPlane(scene);
            groundPlane.Position.Y = -10;

            DemoUtils.InitDefaultLighting(scene);


            var texture = TextureLoader.Load("../../../assets/textures/dds/test-dxt1.jpg");

            scene.Add(new AmbientLight(new THREE.Math.Color(0x444444)));

            var polyhedron = new IcosahedronBufferGeometry(8, 0);
            polyhedronMesh = AddGeometry(scene, polyhedron, "polyhedron", texture);
            polyhedronMesh.Position.X = 20;

            var sphere = new SphereBufferGeometry(5, 20, 20);
            sphereMesh = AddGeometry(scene, sphere, "sphere", texture);

            var cube = new BoxBufferGeometry(10, 10, 10);
            cubeMesh = AddGeometry(scene, cube, "cube", texture);
            cubeMesh.Position.X = -20;
        }
    }
}
