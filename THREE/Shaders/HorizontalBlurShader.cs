﻿using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class HorizontalBlurShader : ShaderMaterial
    {
        public HorizontalBlurShader() : base()
        {
            Uniforms.Add("tDiffuse", new GLUniform { { "value", null } });
            Uniforms.Add("h", new GLUniform { { "value", 1.0f/512.0f } });

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
			uniform float h;

			varying vec2 vUv;

			void main() {

				vec4 sum = vec4( 0.0 );

				sum += texture2D( tDiffuse, vec2( vUv.x - 4.0 * h, vUv.y ) ) * 0.051;
				sum += texture2D( tDiffuse, vec2( vUv.x - 3.0 * h, vUv.y ) ) * 0.0918;
				sum += texture2D( tDiffuse, vec2( vUv.x - 2.0 * h, vUv.y ) ) * 0.12245;
				sum += texture2D( tDiffuse, vec2( vUv.x - 1.0 * h, vUv.y ) ) * 0.1531;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y ) ) * 0.1633;
				sum += texture2D( tDiffuse, vec2( vUv.x + 1.0 * h, vUv.y ) ) * 0.1531;
				sum += texture2D( tDiffuse, vec2( vUv.x + 2.0 * h, vUv.y ) ) * 0.12245;
				sum += texture2D( tDiffuse, vec2( vUv.x + 3.0 * h, vUv.y ) ) * 0.0918;
				sum += texture2D( tDiffuse, vec2( vUv.x + 4.0 * h, vUv.y ) ) * 0.051;

				gl_FragColor = sum;

			}
		";
        }
    }
}
