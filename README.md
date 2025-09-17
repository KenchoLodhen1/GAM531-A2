# Vector & Matrix Demo
## Features
- Vector3D class with:
  - Addition, subtraction
  - Dot & cross product
  - Magnitude & normalization

- Matrix4x4 class with:
  - Identity, scaling, rotation (X/Y/Z), translation
  - Matrix multiplication
  - Transforming vectors, points, and directions

## Demo Program
The console app (`Program.cs`) shows:
- Vector operations (add, subtract, dot, cross, normalize)
- Matrix operations (scale, rotate, translate, multiply)
- Applying transformations to vectors/points

## Build & Run
1. Open the project in **Visual Studio** or run with `dotnet` CLI.
2. Build and run:
   ```bash
   dotnet run
   ```

## Example Output
```
== Vector Ops ==
v1 = (3.00, 4.00, 5.00)
v2 = (1.00, 2.00, 3.00)
v1 + v2 = (4.00, 6.00, 8.00)
v1 - v2 = (2.00, 2.00, 2.00)
dot(v1,v2) = 26.00
cross(v1,v2) = (-2.00, 4.00, -2.00)
|v1| = 7.07
normalized v1 = (0.42, 0.57, 0.71)
```

---
Lightweight, self-contained, and easy to extend for graphics or game dev.
