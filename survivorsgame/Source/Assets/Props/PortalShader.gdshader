shader_type canvas_item;

uniform float spiral_effect_intensity: hint_range(0.0, 2.0, 0.1) = 0.5;
uniform float rotation_speed: hint_range(0.0, 1.0, 0.05) = 0.1;
uniform float slices: hint_range(0.0, 24.0, 1.0) = 16.0;
uniform vec3 background_color: source_color = vec3(0.7, 0.3, 0.7);
uniform vec3 slice_color: source_color = vec3(0.0, 0.0, 0.0);
uniform float noise_offset_amplitude: hint_range(0.0, 1.0, 0.01) = 0.04;
uniform float noise_oscillation_period = 2.0;
uniform vec2 noise_clamp = vec2(0.25, 0.75);
// uniform float roundness: hint_range(0.0, 1.0, 0.02) = 0.0;
uniform sampler2D noise;

void fragment() {
	float base_angle = atan(UV.y - 0.5, UV.x - 0.5) + TIME * rotation_speed;
	float angle_of_single_slice = radians(360.0 / slices);

	float distance_from_center = length(UV - vec2(0.5, 0.5));
	float spiral_offset = distance_from_center * spiral_effect_intensity;

	float angle = base_angle + spiral_offset;
	float slice_index = round(angle / angle_of_single_slice);

	float time_offset = noise_offset_amplitude * (sin(TIME) + cos(TIME / 2.0));
	ivec2 noiseSize = textureSize(noise, 0);
	vec2 offsetVector = vec2(time_offset, time_offset); // TODO: advance this
	vec4 noise_sample = texture(noise, mod(UV + offsetVector, 1.0));

	vec3 noise_addition = clamp(noise_sample.xyz, noise_clamp.x, noise_clamp.y);

	if (mod(slice_index, 2.0) <= 0.0) {
		// Subtract
		COLOR = vec4(
			slice_color * noise_addition * 1.05,
			1.0
		);
	} else {
		noise_addition *= 1.2;
		// Lighten only
		COLOR = vec4(
			max(background_color.r, noise_addition.r),
			max(background_color.g, noise_addition.g),
			max(background_color.b, noise_addition.b),
			1.0
		);
	}
	COLOR = clamp(COLOR, vec4(0), vec4(1));
}
