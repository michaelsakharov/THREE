﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Cameras;
using THREE.Scenes;

namespace THREE.Renderers.gl
{
    public class GLRenderLists : Hashtable
    {
        public GLRenderList Get(Scene scene, Camera camera)
        {
            Hashtable cameras = null;
            GLRenderList list = null;

            if (!this.ContainsKey(scene))
            {
                list = new GLRenderList();

                cameras = new Hashtable();

                cameras.Add(camera, list);

                this.Add(scene, cameras);
            }
            else
            {
                cameras = (Hashtable)this[scene];

                if (!cameras.ContainsKey(camera))
                {
                    list = new GLRenderList();                    
                    cameras.Add(camera, list);
                    this[scene] = cameras;
                }
                else
                {
                    list = (GLRenderList)cameras[camera];
                }
            }

            return list;
        }
    }
}
