﻿using System;
using THREE;
using THREE.Cameras;
using THREE.Math;
using THREE.Scenes;
using THREE.Helpers;
using THREE.Geometries;
using THREE.Materials;
using THREE.Objects;
using OpenTK;
using THREE.Controls;
using THREE.Lights;
using THREE.Core;
using Vector3 = THREE.Math.Vector3;
using Matrix4 = THREE.Math.Matrix4;
using Quaternion = THREE.Math.Quaternion;
using System.Collections;
using System.Collections.Generic;
using Vector2 = THREE.Math.Vector2;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using THREE.Loaders;
using System.Diagnostics;

namespace THREEExample.Three.Misc.Controls
{
    [Example("Controls-Transform", ExampleCategory.Misc, "Controls")]
    public class TransControlsExample : ControlsExampleTemplate
    {
       
        TransformControls transformControls;

        public TransControlsExample() : base()
        {
           
        }
     
        public override void Init()
        {
            base.Init();

            scene.Add(new GridHelper(1000, 10, 0x888888, 0x444444));


            var texture = TextureLoader.Load(@"../../../assets/textures/crate.gif");
            texture.Anisotropy = renderer.capabilities.GetMaxAnisotropy();
            
            var geometry = new BoxGeometry(200, 200, 200);
            var material = new MeshLambertMaterial();
            material.Transparent = true;
            material.Map = texture;
            material.DepthTest = false;
            material.DepthWrite = false;
            material.ToneMapped = false;
           
            var mesh = new Mesh(geometry, material);
            scene.Add(mesh);

            transformControls = new TransformControls(glControl,camera);
            transformControls.PropertyChanged += OnPropertyChanged;
            transformControls.Attach(mesh);

            scene.Add(transformControls);


        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("dragging"))
            {
                controls.state = TrackballControls.STATE.NONE;
                controls.Enabled = !(sender as TransformControls).dragging;
               
            }
        }       
    }
}
