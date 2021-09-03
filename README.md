# COMP30019 Assignment 1 - Ray Tracer

**Name:** Yun-Chi Hsiao \
**Student Number:** 1074004 \
**Username:** yunchi \
**Email:** yunchi@student.unimelb.edu.au

## Completed stages
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

## Final scene render

![My final render](/images/final_scene.png)

This render took **698** minutes and **59** seconds on my PC.

I used the following command to render the image exactly as shown:

```
dotnet run -- -f tests/final_scene.txt -o ./images/final_scene.png -l -x4
```

## Sample outputs

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
