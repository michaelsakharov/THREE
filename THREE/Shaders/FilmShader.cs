﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Materials;
using THREE.Renderers.gl;

namespace THREE.Shaders
{
    public class FilmShader : ShaderMaterial
    {
        public FilmShader()
        {
            Uniforms.Add("tDiffuse", new GLUniform { { "value", null } });
            Uniforms.Add("time", new GLUniform { { "value", 0.0f } });
            Uniforms.Add("nIntensity", new GLUniform { { "value", 0.5f } });
            Uniforms.Add("sIntensity", new GLUniform { { "value", 0.05f } });
            Uniforms.Add("sCount", new GLUniform { { "value", 4096 } });
            Uniforms.Add("grayscale", new GLUniform { { "value", 1 } });

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

// control parameter
	uniform float time;

uniform bool grayscale;

// noise effect intensity value (0 = no effect, 1 = full effect)
uniform float nIntensity;

// scanlines effect intensity value (0 = no effect, 1 = full effect)
uniform float sIntensity;

// scanlines effect count value (0 = no effect, 4096 = full effect)
uniform float sCount;

uniform sampler2D tDiffuse;

varying vec2 vUv;

void main() {

// sample the source
	vec4 cTextureScreen = texture2D( tDiffuse, vUv );

// make some noise
	float dx = rand( vUv + time );

// add noise
	vec3 cResult = cTextureScreen.rgb + cTextureScreen.rgb * clamp( 0.1 + dx, 0.0, 1.0 );

// get us a sine and cosine
	vec2 sc = vec2( sin( vUv.y * sCount ), cos( vUv.y * sCount ) );

// add scanlines
	cResult += cTextureScreen.rgb * vec3( sc.x, sc.y, sc.x ) * sIntensity;

// interpolate between source and result by intensity
	cResult = cTextureScreen.rgb + clamp( nIntensity, 0.0,1.0 ) * ( cResult - cTextureScreen.rgb );

// convert to grayscale if desired
	if( grayscale ) {

		cResult = vec3( cResult.r * 0.3 + cResult.g * 0.59 + cResult.b * 0.11 );

	}

	gl_FragColor =  vec4( cResult, cTextureScreen.a );

}
"
;


		}
    }
}
