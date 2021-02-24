<h1 align="center">SkiaInk :pencil2:</h1>

<h3 align="center">:wavy_dash: A C# Digital Ink Library for Skia :wavy_dash:</h3>

## About

SkiaInk is meant to be a lightweight and user-friendly library for creating, rendering, and manipulating digital ink. The API is inspired by [Windows.UI.Input.Inking](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Input.Inking?view=winrt-19041)
and [Wacom Will SDK](https://developer-docs.wacom.com/sdk-for-ink/docs/overview).

## Roadmap

### V0.1 - Rendering Digital Ink

- [ ] Basic API
- [ ] Vector Brush
- [ ] Geometry pipeline

### V0.2 - Creating Digital Ink

- [ ] Velocity prediction (to reduce latency)
- [ ] Path smoothing input

### V0.3 - Manipulating Digital Ink
- [ ] Partial Eraser
- [ ] Hit testing

### Future

- [ ] Ink playback animation
- [ ] Raster Brush
- [ ] Default ink types such as pencil, highlighter, pen, calligraphy, etc.
- [ ] Download as nuget package

## Geometry Pipeline

![Ink Pipeline](/Docs/InkPipeline.svg)

> For now focusing on vector brush pipeline. Future plans for raster ink will have differing pipeline

<details>
  <summary>Details</summary>
  <p>

### Path Smoothing

Use double exponential smoothing for user input. Will have the ability to turn off.

### Spline Interpolation

Spline production/interpolation are combined as SkiaInk uses xsplines. An xspline with shape = -1, which is what SkiaInk uses for inkstrokes, is very similar to a Catmull-Rom curve.

XSplines are outlined in this paper: [XSplines: A Spline Model Designed for the End User](https://static.aminer.org/pdf/PDF/000/593/089/x_splines_a_spline_model_designed_for_the_end_user.pdf)

Implementation of algorithm is ported from R source code: [xspline.c](https://github.com/wch/r-source/blob/trunk/src/main/xspline.c)

### Brush Applier

Places the chosen brush on each point of the xspline line according to pressure. Interpolate the shape of the brush when applying to interpolated points

Interpolation algorithm: [SkiaSharp.Extended source](https://github.com/mono/SkiaSharp.Extended/blob/main/source/SkiaSharp.Extended/PathInterpolation/SKPathInterpolation.cs)?
Drawn inspiration from [vwline](https://github.com/pmur002/vwline) package.

### Convex Hull Chain

Applies a modified convex hull algorithm (see: [StackOverflow](https://stackoverflow.com/questions/18619117/combine-nearby-polygons)) to every two adjacent brushes in a chain

Using [Ouellet Convex Hull](https://www.codeproject.com/Articles/1232301/First-and-Extremely-fast-Online-D-Convex-Hull-Algo) for base convex hull agorithm. The github [repo](https://github.com/EricOuellet2/ConvexHull). Specifically using their [Avl3](https://github.com/EricOuellet2/ConvexHull/tree/master/OuelletConvexHullAvl3) algorithm.

### Polyline Clipper

Performs a union of all the convex hulls to output the ink stroke outline in polyline form.

Either use the C# implementation of [Clipper](http://angusj.com/delphi/clipper.php) or use SkiaSharp paths

### Polyline Simplifier

Simplify the number of points along the outline using the [Ramer-Douglas-Peucker](https://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm) algorithm

Maybe use this implementation: https://github.com/BobLd/RamerDouglasPeuckerNetV2

  </p>
</details>
