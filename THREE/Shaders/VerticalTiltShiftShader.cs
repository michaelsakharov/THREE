﻿using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class VerticalTiltShiftShader : ShaderMaterial
    {
        public VerticalTiltShiftShader() : base()
        {
            Uniforms.Add("tDiffuse", new GLUniform { { "value", null } });
            Uniforms.Add("v", new GLUniform { { "value", 1.0f/512.0f } });
			Uniforms.Add("r", new GLUniform { { "value", 0.35f } });

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
				uniform float r;

				varying vec2 vUv;

				void main() {

					vec4 sum = vec4( 0.0 );

					float vv = v * abs( r - vUv.y );

					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 4.0 * vv ) ) * 0.051;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 3.0 * vv ) ) * 0.0918;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 2.0 * vv ) ) * 0.12245;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y - 1.0 * vv ) ) * 0.1531;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y ) ) * 0.1633;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 1.0 * vv ) ) * 0.1531;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 2.0 * vv ) ) * 0.12245;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 3.0 * vv ) ) * 0.0918;
					sum += texture2D( tDiffuse, vec2( vUv.x, vUv.y + 4.0 * vv ) ) * 0.051;

					gl_FragColor = sum;

				}
		";
        }
    }
}
