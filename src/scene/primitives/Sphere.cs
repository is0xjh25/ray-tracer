using System;

namespace RayTracer
{
    /// <summary>
    /// Class to represent an (infinite) plane in a scene.
    /// </summary>
    public class Sphere : SceneEntity
    {
        private Vector3 center;
        private double radius;
        private Material material;

        /// <summary>
        /// Construct a sphere given its center point and a radius.
        /// </summary>
        /// <param name="center">Center of the sphere</param>
        /// <param name="radius">Radius of the spher</param>
        /// <param name="material">Material assigned to the sphere</param>
        public Sphere(Vector3 center, double radius, Material material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
        }

        /// <summary>
        /// Determine if a ray intersects with the sphere, and if so, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray) 
        {   
            Vector3 oc = ray.Origin - this.center;
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2 * oc.Dot(ray.Direction);
            double c = oc.Dot(oc) - this.radius * this.radius;
            double t;

            double discriminant = b * b - 4 * a * c;
            
            if (discriminant < 0) return null;
            
            // Try first point.
            double numerator = -b - Math.Sqrt(discriminant);
            if (numerator > 0) 
            {
                t =  numerator / (2 * a);
                
            } 
            else 
            {
                //Try second point.
                numerator = -b + Math.Sqrt(discriminant);
                
                if (numerator > 0) 
                {
                    t = numerator / (2.0 * a);
                } 
                else 
                {
                    return null;
                }
            }
            
            // Invalid backward.
            if (t <= 0) return null;
            
            // p = o + td
            Vector3 position = ray.Origin + t * ray.Direction;

            // Caculate normal.
            Vector3 normal = (position - this.center).Normalized();
 
            return new RayHit (position, normal, ray.Direction, this.material);
        }


        /// <summary>
        /// The material of the sphere.
        /// </summary>
        public Material Material { get { return this.material; } }
    }

}
