﻿using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class MirrorShader : ShaderMaterial
    {
        public MirrorShader() : base()
        {
            Uniforms.Add("tDiffuse", new GLUniform { { "value", null } });
            Uniforms.Add("side", new GLUniform { { "value", 1 } });

            VertexShader = @"
                varying vec2 vUv; 


                void main() {

					vUv = uv;
			        gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );

		        }


            "
            ;

            FragmentShader = @"
				uniform sampler2D tDiffuse; 
				uniform int side;

				varying vec2 vUv;

				void main() {

					vec2 p = vUv;
					if (side == 0){
						if (p.x > 0.5) p.x = 1.0 - p.x;
					}else if (side == 1){
						if (p.x < 0.5) p.x = 1.0 - p.x;
					}else if (side == 2){
						if (p.y < 0.5) p.y = 1.0 - p.y;
					}else if (side == 3){
						if (p.y > 0.5) p.y = 1.0 - p.y;
					} 
					vec4 color = texture2D(tDiffuse, p);
					gl_FragColor = color;

				}


			";
        }
    }
}
