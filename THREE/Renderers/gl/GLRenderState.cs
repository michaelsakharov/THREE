﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Cameras;
using THREE.Lights;
using THREE.Renderers.gl;

namespace THREE.Renderers.gl
{
    
    public class GLRenderState
    {
        private List<Light> lightsArray = new List<Light>();
        
        private List<Light> shadowsArray = new List<Light>();

        private GLLights lights = new GLLights();

        public struct RenderState
        {
            public List<Light> LightsArray;

            public List<Light> ShadowsArray;

            public GLLights Lights;
        }

        public RenderState State;

        public GLRenderState()
        {
            State.LightsArray = lightsArray;
            State.ShadowsArray = shadowsArray;
            State.Lights = lights;
        }

        public void Init()
        {
            lightsArray.Clear();
            shadowsArray.Clear();
        }

        public void SetupLights(Camera camera)
        {
            lights.Setup(lightsArray,camera);
        }

        public void PushLight(Light light)
        {
            lightsArray.Add(light);
        }

        public void PushShadow(Light shadowLight)
        {
            shadowsArray.Add(shadowLight);
        }
    }
}
