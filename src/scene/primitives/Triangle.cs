using System;

namespace RayTracer
{
    /// <summary>
    /// Class to represent a triangle in a scene represented by three vertices.
    /// </summary>
    public class Triangle : SceneEntity
    {
        private Vector3 v0, v1, v2;
        private Material material;

        /// <summary>
        /// Construct a triangle object given three vertices.
        /// </summary>
        /// <param name="v0">First vertex position</param>
        /// <param name="v1">Second vertex position</param>
        /// <param name="v2">Third vertex position</param>
        /// <param name="material">Material assigned to the triangle</param>
        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, Material material)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.material = material;
        }

        /// <summary>
        /// Determine if a ray intersects with the triangle, and if so, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {   
            Vector3 normal = ((this.v1 - this.v0).Cross(this.v2 - this.v0)).Normalized();
            
            // Ray and entity are parallel.
            if (Math.Abs(ray.Direction.Dot(normal)) < Double.Epsilon) return null;
            
            // t = (p-o) dot N / d dot N
            double t = (this.v0 - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);

            // p = o + td
            Vector3 position = ray.Origin + t * ray.Direction;
            
            // Invalid backward.
            if (t <= 0) return null;
            
            // Inside outside test.
            Vector3 C; // vector perpendicular to triangle's plane
                
            // Edge 0
            Vector3 edge0 = v1 - v0; 
            Vector3 vp0 = position - v0; 
            C = edge0.Cross(vp0);
            // P is on the right side 
            if (normal.Dot(C) < 0) return null;

            // Edge 1
            Vector3 edge1 = v2 - v1; 
            Vector3 vp1 = position - v1; 
            C = edge1.Cross(vp1);
            if (normal.Dot(C) < 0) return null;

            // Edge 2
            Vector3 edge2 = v0 - v2; 
            Vector3 vp2 = position - v2; 
            C = edge2.Cross(vp2);
            if (normal.Dot(C) < 0) return null;

            return new RayHit (position, normal, ray.Direction, this.material);
        } 
         
        /// <summary>
        /// The material of the triangle.
        /// </summary>
        public Material Material { get { return this.material; }}     
    }
}
