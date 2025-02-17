﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class BokehShader : ShaderMaterial
    {
        public BokehShader() : base()
        {
            Defines.Add("DEPTH_PACKING", "1");
            Defines.Add("PERSPECTIVE_CAMERA", "1");

            Uniforms.Add("tColor", new GLUniform { { "value", null } });
            Uniforms.Add("tDepth", new GLUniform { { "value", null } });
		    Uniforms.Add("focus", new GLUniform{ {"value", 1.0f }});
            Uniforms.Add("aspect", new GLUniform { { "value", 1.0f } });
	        Uniforms.Add("aperture", new GLUniform { { "value", 0.025f } });
	        Uniforms.Add("maxblur", new GLUniform { { "value", 0.01f } });
	        Uniforms.Add("nearClip", new GLUniform { { "value", 1.0f } });
	        Uniforms.Add("farClip", new GLUniform { { "value", 1000.0f } });


            VertexShader = @"
                varying vec2 vUv; 


                        void main() {

			                vUv = uv;
			                gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );

		                }


                "
            ;

            FragmentShader = @"
			#include <common>

			varying vec2 vUv;

			uniform sampler2D tColor;
			uniform sampler2D tDepth;

			uniform float maxblur; // max blur amount
			uniform float aperture; // aperture - bigger values for shallower depth of field

			uniform float nearClip;
			uniform float farClip;

			uniform float focus;
			uniform float aspect;

			#include <packing>

			float getDepth( const in vec2 screenPosition ) {
				#if DEPTH_PACKING == 1
				return unpackRGBAToDepth( texture2D( tDepth, screenPosition ) );
				#else
				return texture2D( tDepth, screenPosition ).x;
				#endif
			}

			float getViewZ( const in float depth ) {
				#if PERSPECTIVE_CAMERA == 1
				return perspectiveDepthToViewZ( depth, nearClip, farClip );
				#else
				return orthographicDepthToViewZ( depth, nearClip, farClip );
				#endif
			}


			void main() {

				vec2 aspectcorrect = vec2( 1.0, aspect );

				float viewZ = getViewZ( getDepth( vUv ) );

				float factor = ( focus + viewZ ); // viewZ is <= 0, so this is a difference equation

				vec2 dofblur = vec2 ( clamp( factor * aperture, -maxblur, maxblur ) );

				vec2 dofblur9 = dofblur * 0.9;
				vec2 dofblur7 = dofblur * 0.7;
				vec2 dofblur4 = dofblur * 0.4;

				vec4 col = vec4( 0.0 );

				col += texture2D( tColor, vUv.xy );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,   0.4  ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.15,  0.37 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.29,  0.29 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.37,  0.15 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.40,  0.0  ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.37, -0.15 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.29, -0.29 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.15, -0.37 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,  -0.4  ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.15,  0.37 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29,  0.29 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.37,  0.15 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.4,   0.0  ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.37, -0.15 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29, -0.29 ) * aspectcorrect ) * dofblur );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.15, -0.37 ) * aspectcorrect ) * dofblur );

				col += texture2D( tColor, vUv.xy + ( vec2(  0.15,  0.37 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.37,  0.15 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.37, -0.15 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.15, -0.37 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.15,  0.37 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.37,  0.15 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.37, -0.15 ) * aspectcorrect ) * dofblur9 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.15, -0.37 ) * aspectcorrect ) * dofblur9 );

				col += texture2D( tColor, vUv.xy + ( vec2(  0.29,  0.29 ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.40,  0.0  ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.29, -0.29 ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,  -0.4  ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29,  0.29 ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.4,   0.0  ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29, -0.29 ) * aspectcorrect ) * dofblur7 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,   0.4  ) * aspectcorrect ) * dofblur7 );

				col += texture2D( tColor, vUv.xy + ( vec2(  0.29,  0.29 ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.4,   0.0  ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.29, -0.29 ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,  -0.4  ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29,  0.29 ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.4,   0.0  ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2( -0.29, -0.29 ) * aspectcorrect ) * dofblur4 );
				col += texture2D( tColor, vUv.xy + ( vec2(  0.0,   0.4  ) * aspectcorrect ) * dofblur4 );

				gl_FragColor = col / 41.0;
				gl_FragColor.a = 1.0;

			}


";

        }
    }
}
