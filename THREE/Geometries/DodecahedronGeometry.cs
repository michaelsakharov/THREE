﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Core;

namespace THREE.Geometries
{
    public class DodecahedronGeometry : Geometry
    {
        public Hashtable parameters;

        public DodecahedronGeometry(float? radius=null,float? detail=null) : base()
        {
            parameters = new Hashtable()
            {
                {"radius",radius },
                {"detail",detail }
            };

            this.FromBufferGeometry(new DodecahedronBufferGeometry(radius, detail));
            this.MergeVertices();
        }

    }

    public class DodecahedronBufferGeometry : PolyhedronBufferGeometry
    {
		static float t = (1 + (float)System.Math.Sqrt(5)) / 2;

		static float r = 1 / t;

		static List<float>  vertices = new List<float>
		{
				// (±1, ±1, ±1)
				-1, -1, -1, -1, -1, 1,
				-1, 1, -1, -1, 1, 1,
				1, -1, -1, 1, -1, 1,
				1, 1, -1, 1, 1, 1,

				 // (0, ±1/φ, ±φ)
				 0, -r, -t, 0, -r, t,
				 0, r, -t, 0, r, t,

				// (±1/φ, ±φ, 0)
				-r, -t, 0, -r, t, 0,
				 r, -t, 0, r, t, 0,

				// (±φ, 0, ±1/φ)
				-t, 0, -r, t, 0, -r,
				-t, 0, r, t, 0, r
			};

		static List<int> indices = new List<int>
		{
			3, 11, 7, 3, 7, 15, 3, 15, 13,
			7, 19, 17, 7, 17, 6, 7, 6, 15,
			17, 4, 8, 17, 8, 10, 17, 10, 6,
			8, 0, 16, 8, 16, 2, 8, 2, 10,
			0, 12, 1, 0, 1, 18, 0, 18, 16,
			6, 10, 2, 6, 2, 13, 6, 13, 15,
			2, 16, 18, 2, 18, 3, 2, 3, 13,
			18, 1, 9, 18, 9, 11, 18, 11, 3,
			4, 14, 12, 4, 12, 0, 4, 0, 8,
			11, 9, 5, 11, 5, 19, 11, 19, 7,
			19, 5, 14, 19, 14, 4, 19, 4, 17,
			1, 12, 14, 1, 14, 5, 1, 5, 9
		};

		public DodecahedronBufferGeometry(float? radius=null,float? detail=null) : base(vertices, indices, radius, detail)
		{

		}
	}
}
