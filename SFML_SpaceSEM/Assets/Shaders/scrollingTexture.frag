uniform sampler2D texture;
uniform vec2 u_time;
uniform float scrollRate;

void main()
{
    // lookup the pixel in the texture
    vec4 pixel = texture2D(texture, vec2(gl_TexCoord[0].x, gl_TexCoord[0].y + (u_time.y * scrollRate)));

    // multiply it by the color
    gl_FragColor = gl_Color * pixel;
}