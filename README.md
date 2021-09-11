# COMP30019 Assignment 1 - Ray Tracer
This is your README.md... you should write anything relevant to your implementation here.

Please ensure your student details are specified below (*exactly* as on UniMelb records):

**Name:** Yun-Chi Hsiao \
**Student Number:** 1074004 \
**Username:** yunchi \
**Email:** yunchi@student.unimelb.edu.au

## Completed stages

Tick the stages bellow that you have completed so we know what to mark (by editing README.md). At most **six** marks can be chosen in total for stage three. If you complete more than this many marks, pick your best one(s) to be marked!

<!---
Tip: To tick, place an x between the square brackes [ ], like so: [x]
-->

##### Stage 1

- [x] Stage 1.1 - Familiarise yourself with the template
- [x] Stage 1.2 - Implement vector mathematics
- [x] Stage 1.3 - Fire a ray for each pixel
- [x] Stage 1.4 - Calculate ray-entity intersections
- [x] Stage 1.5 - Output primitives as solid colours

##### Stage 2

- [x] Stage 2.1 - Diffuse materials
- [x] Stage 2.2 - Shadow rays
- [x] Stage 2.3 - Reflective materials
- [x] Stage 2.4 - Refractive materials
- [x] Stage 2.5 - The Fresnel effect
- [x] Stage 2.6 - Anti-aliasing

##### Stage 3

- [ ] Option A - Emissive materials (+6)
- [x] Option B - Ambient lighting/occlusion (+6)
- [ ] Option C - OBJ models (+6)
- [ ] Option D - Glossy materials (+3)
- [ ] Option E - Custom camera orientation (+3)
- [ ] Option F - Beer's law (+3)
- [ ] Option G - Depth of field (+3)

*Please summarise your approach(es) to stage 3 here.*

To implement ambient lighting/occlusion in Stage 3, Monte Carlo Path Tracing is used for creating global illumination.Generally, rays are randomly generated into a hemisphere of directions oriented around the shading normal. If these rays hit an object, the color at the intersection point is computed and returned to the shading point. All the colors are summed up and divided by the number N of cast samples. Monte Carlo theory also implies to divide each sample by the random variable PDF, and to multiply the object's color by the cosine of the angle between the shading normal and the ray direction. The ambient sample size of the final image is set to eight due to the limit of time (and the anti-aliasing is -x4). If the computational process can be opitmised (it definitely can),
the sample size can be increased as well which leads to having a greater and smoother image with ambient lighting.

## Final scene render

Be sure to replace ```/images/final_scene.png``` with your final render so it shows up here:

![My final render](/images/final_scene.png)

This render took **698** minutes and **59** seconds on my PC.

I used the following command to render the image exactly as shown:

```
dotnet run -- -f tests/final_scene.txt -o ./images/final_scene.png -l -x4
```

## Sample outputs

We have provided you with some sample tests located at ```/tests/*```. So you have some point of comparison, here are the outputs our ray tracer solution produces for given command line inputs (for the first two stages, left and right respectively):

###### Sample 1
```
dotnet run -- -f tests/sample_scene_1.txt -o images/sample_scene_1.png -x 4
```
<p float="left">
  <img src="/images/sample_scene_1_s1.png" />
  <img src="/images/sample_scene_1_s2.png" /> 
</p>

###### Sample 2

```
dotnet run -- -f tests/sample_scene_2.txt -o images/sample_scene_2.png -x 4
```
<p float="left">
  <img src="/images/sample_scene_2_s1.png" />
  <img src="/images/sample_scene_2_s2.png" /> 
</p>

## References

1. Intersection of ray and triangle \
   Scratchapixel 2.0: \
   https://www.scratchapixel.com/lessons/3d-basic-rendering/ray-tracing-rendering-a-triangle/ray-triangle-intersection-geometric-solution

2. Intersection of ray and sphere \
   1000 Forms Of Bunnies Victor's blog: \
   https://viclw17.github.io/2018/07/16/raytracing-ray-sphere-intersection/

3. Reflection, Refraction (Transmission) and Fresnel \
   Scratchapixel 2.0: \
   https://www.scratchapixel.com/lessons/3d-basic-rendering/introduction-to-shading/reflection-refraction-fresnel
   
4. Global Illumination and Path Tracing\
   Scratchapixel 2.0: \
   https://www.scratchapixel.com/lessons/3d-basic-rendering/global-illumination-path-tracing/global-illumination-path-tracing-practical-implementation
