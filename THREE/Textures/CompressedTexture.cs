﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THREE.Textures
{
    public class CompressedTexture : Texture
    {
        private int Width;
        private int Height;
        public CompressedTexture(List<MipMap> mipmaps, int width,int height,int? mapping = null, int? wrapS = null, int? wrapT = null, int? magFilter = null, int? minFilter = null, int? format = null, int? type = null, int? anisotropy = null, int? encoding = null) :
            base(null, mapping, wrapS, wrapT, magFilter, minFilter, format, type, anisotropy, encoding)
        {
            this.Width = width;
            this.Height = height;
            this.Mipmaps = mipmaps;

            this.flipY = false;

            this.GenerateMipmaps = false;

        }
            

    }
}
