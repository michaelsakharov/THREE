﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE;
using THREE.Cameras;
using THREE.Math;
using THREE.Scenes;
using THREE.Helpers;
using THREE.Geometries;
using THREE.Materials;
using THREE.Objects;
using OpenTK;
using THREE.Lights;
using THREEExample.ThreeImGui;
using THREE.Controls;

namespace THREEExample.Learning.Chapter01
{
    [Example("02-Materials-Light", ExampleCategory.LearnThreeJS, "Chapter01")]
    public class MaterialsLightExample : Example
    {
        Scene scene;

        Camera camera;

        TrackballControls controls;

       
        public MaterialsLightExample() : base()
        {
            camera = new PerspectiveCamera();
            scene = new Scene();
        }
        private void InitRenderer()
        {
            this.renderer.SetClearColor(new Color().SetHex(0x000000));
            this.renderer.ShadowMap.Enabled = true;
            this.renderer.ShadowMap.type = Constants.PCFSoftShadowMap;
        }
        private void InitCamera()
        {
            camera.Fov = 45.0f;
            camera.Aspect = this.glControl.AspectRatio;
            camera.Near = 0.1f;
            camera.Far = 1000.0f;
            camera.Position.X = -30;
            camera.Position.Y = 50;
            camera.Position.Z = 40;
            camera.LookAt(THREE.Math.Vector3.Zero());
        }
        private void InitCameraController()
        {
            controls = new TrackballControls(this.glControl, camera);
            controls.StaticMoving = false;
            controls.RotateSpeed = 3.0f;
            controls.ZoomSpeed = 2;
            controls.PanSpeed = 2;
            controls.NoZoom = false;
            controls.NoPan = false;
            controls.NoRotate = false;
            controls.StaticMoving = true;
            controls.DynamicDampingFactor = 0.2f;
        }
        public override void Load(GLControl glControl)
        {
            base.Load(glControl);

            InitRenderer();

            InitCamera();

            InitCameraController();

           

            scene.Background = Color.Hex(0xffffff);

            var axes = new AxesHelper(20);

            scene.Add(axes);

            var planeGeometry = new PlaneGeometry(60, 20);
            var planeMaterial = new MeshLambertMaterial() { Color = Color.Hex(0xcccccc) };
            var plane = new Mesh(planeGeometry, planeMaterial);

            plane.Rotation.X = (float)(-0.5 * Math.PI);
            plane.Position.Set(15, 0, 0);

            scene.Add(plane);

            // create a cube
            var cubeGeometry = new BoxGeometry(4, 4, 4);
            var cubeMaterial = new MeshLambertMaterial() { Color = Color.Hex(0xff0000) };
            var cube = new Mesh(cubeGeometry, cubeMaterial);

            // position the cube
            cube.Position.Set(-4, 3, 0);

            // add the cube to the scene

            scene.Add(cube);

            //      // create a sphere
            var sphereGeometry = new SphereGeometry(4, 20, 20);
            var sphereMaterial = new MeshLambertMaterial() { Color = Color.Hex(0x7777ff) };
            var sphere = new Mesh(sphereGeometry, sphereMaterial);

            //      // position the sphere
            sphere.Position.Set(20, 4, 2);

            //      // add the sphere to the scene
            scene.Add(sphere);

            var spotLight = new SpotLight(new Color().SetHex(0xffffff));

            spotLight.Position.Set(-40, 60, -10);
            spotLight.CastShadow = true;

            scene.Add(spotLight);

        }
        public override void Render()
        {
          

            this.renderer.Render(scene, camera);

            
        }
        public override void Resize(System.Drawing.Size clientSize)
        {
            base.Resize(clientSize);
            camera.Aspect = this.glControl.AspectRatio;
            camera.UpdateProjectionMatrix();
        }
    }
}
