﻿using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class VerticalBlurShader : ShaderMaterial
    {
        public VerticalBlurShader() : base()
        {
            Uniforms.Add("tDiffuse", new GLUniform { { "value", null } });
            Uniforms.Add("v", new GLUniform { { "value", 1.0f/512.0f } });

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
			uniform float v;

			varying vec2 vUv;

			void main() {

				vec4 sum = vec4( 0.0 );

				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 4.0 * v ) ) * 0.051;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 3.0 * v ) ) * 0.0918;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 2.0 * v ) ) * 0.12245;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 1.0 * v ) ) * 0.1531;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y ) ) * 0.1633;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 1.0 * v ) ) * 0.1531;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 2.0 * v ) ) * 0.12245;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 3.0 * v ) ) * 0.0918;
				sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 4.0 * v ) ) * 0.051;

				gl_FragColor = sum;

			}
		";
        }
    }
}
