﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THREE.Textures
{
    public class DataTexture2DArray : Texture
    {
        public int Width;
        public int Height;
        public int Depth;

        public byte[] Data;

        public DataTexture2DArray(byte[] array, int width, int height, int depth) : base()
        {
            this.Data = array;
            this.Width = width;
            this.Height = height;

            this.WrapR = Constants.ClampToEdgeWrapping;

            this.GenerateMipmaps = false;

            this.flipY = false;

            this.NeedsUpdate = true;
        }
    }
}
