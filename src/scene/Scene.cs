using System;
using System.Collections.Generic;

namespace RayTracer
{
    /// <summary>
    /// Class to represent a ray traced scene, including the objects,
    /// light sources, and associated rendering logic.
    /// </summary>
    public class Scene
    {
        private SceneOptions options;
        private ISet<SceneEntity> entities;
        private ISet<PointLight> lights;

        /// <summary>
        /// Construct a new scene with provided options.
        /// </summary>
        /// <param name="options">Options data</param>
        public Scene(SceneOptions options = new SceneOptions())
        {
            this.options = options;
            this.entities = new HashSet<SceneEntity>();
            this.lights = new HashSet<PointLight>();
        }

        /// <summary>
        /// Add an entity to the scene that should be rendered.
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void AddEntity(SceneEntity entity)
        {
            this.entities.Add(entity);
        }

        /// <summary>
        /// Add a point light to the scene that should be computed.
        /// </summary>
        /// <param name="light">Light structure</param>
        public void AddPointLight(PointLight light)
        {
            this.lights.Add(light);
        }

        /// <summary>
        /// Render the scene to an output image. This is where the bulk
        /// of your ray tracing logic should go... though you may wish to
        /// break it down into multiple functions as it gets more complex!
        /// </summary>
        /// <param name="outputImage">Image to store render output</param>
        public void Render(Image outputImage)
        {   
            //DateTime start = DateTime.Now;   
            Vector3 origin = new Vector3(0, 0, 0);
            Vector3 direction;
            Ray ray;
            double fov_degree = 60;
            double fov_radians = fov_degree * (Math.PI / 180);
            int recurseTime = 0;
            int recurseLimit = 4;
            Color pixelColor = new Color(0, 0, 0); 
            int multi = this.options.AAMultiplier;

            // Generate rays for each pixel.
            for (int y = 0; y < outputImage.Height; y++) 
            {
                for (int x = 0; x < outputImage.Width; x++) 
                {   
                    double pixelX = (x + 0.5) / outputImage.Width;
                    double pixelY = (y + 0.5) / outputImage.Height;
                    
                    // Location of Projection.
                    double xPos = pixelX * 2 - 1;
                    double yPos = 1 - (pixelY * 2);
                    double zPos = 1;
                    xPos = xPos * Math.Tan(fov_radians / 2);
                    yPos = yPos * (Math.Tan(fov_radians / 2) / (outputImage.Width / outputImage.Height));
                    
                    if (multi == 1)
                    {
                        direction = new Vector3(xPos, yPos, zPos).Normalized();
                        ray = new Ray(origin, direction);
                        pixelColor = RayTrace(ray, recurseTime, recurseLimit);
                    }
                    else 
                    {   
                        // Anti-aliasing
                        for (int i = 1; i <= multi; i++)
                        {
                            for (int j = 1; j <= multi; j++)
                            {   
                                double xDir = i % 2 == 0 ? 1 : -1;
                                double yDir = j % 2 == 0 ? 1 : -1;     
                                double xMove = (i * xDir) / (outputImage.Width * multi * 2);
                                double yMove = (j * yDir) / (outputImage.Height * multi * 2);                              
                                direction = new Vector3(xPos + xMove, yPos + yMove, zPos).Normalized();
                                ray = new Ray(origin, direction);
                                pixelColor += RayTrace(ray, recurseTime, recurseLimit);
                            }
                        }
                        pixelColor = pixelColor / (multi * multi);
                    }              

                    // Output Image
                    pixelColor = NormalizedColor(pixelColor);
                    outputImage.SetPixel(x, y, pixelColor);
                }
            }
            
            // Executuion Time
            //DateTime end = DateTime.Now;
            //TimeSpan ts = (end - start);
            //Console.WriteLine("Elapsed Time is {0} s", ts.TotalSeconds);
        }

        public Color RayTrace(Ray ray, int recurseTime, int recurseLimit)
        {   
            Ray newRay;
            Vector3 newOrigin;
            Vector3 newDirection;
            Color pixelColor = new Color(0, 0, 0);
            Color reflectColor = new Color(0, 0, 0);
            Color refractColor = new Color(0, 0, 0);
            double distSq = Double.PositiveInfinity;
            SceneEntity realEntity = null;
            RayHit realHit = null;
            double reflectRatio = 0;
            // Stage 3 - Option B
            Boolean ambient = this.options.AmbientLightingEnabled;

            if (recurseTime == recurseLimit) return (new Color(0, 0, 0));

            // Loop all entities and find the closest hit object.
            foreach (SceneEntity entity in this.entities) 
            {
                RayHit hit = entity.Intersect(ray);
                    
                if (hit != null) 
                {                                
                    // Choose the front most entity.
                    if ((hit.Position - ray.Origin).LengthSq() < distSq) 
                    {   
                        realHit = hit;
                        realEntity = entity;
                        distSq = (hit.Position - ray.Origin).LengthSq();
                    }
                }        
            }

            if (realHit == null) 
            {
                return pixelColor;
            } 

            // Offset
            bool outside = realHit.Incident.Dot(realHit.Normal) < 0; 
            Vector3 offset = realHit.Normal * 0.0000000001;
               
            // Diffusion
            if (!ambient && realEntity.Material.Type == Material.MaterialType.Diffuse)
            {   
                foreach (PointLight light in lights)
                {   
                    if (!InShadow(realEntity, light, realHit))
                    {
                        // C = (N dot L) * Cm * Cl
                        Vector3 N = realHit.Normal;
                        Vector3 L = (light.Position - realHit.Position).Normalized();
                        pixelColor += realEntity.Material.Color * light.Color * N.Dot(L);
                    }
                }
                return pixelColor;
            }

            // Diffusion - Ambient Light
            if (ambient && realEntity.Material.Type == Material.MaterialType.Diffuse)
            {   
                Color directLight = new Color(0, 0, 0);
                Color inDirectLight = new Color(0, 0, 0);
                Vector3 L;
                Vector3 N;
                Vector3 Nt;
                Vector3 Nb;
                    
                // Compute direct illumination.
                foreach (PointLight light in lights)
                {   
                    if (!InShadow(realEntity, light, realHit))
                    {
                        // C = (N dot L) * Cm * Cl
                        N = realHit.Normal;
                        L = (light.Position - realHit.Position).Normalized();
                        directLight += realEntity.Material.Color * light.Color * N.Dot(L);
                    }
                }
                    
                // Compute indirect illumination.
                // Compute a rotation matrix.
                var rand = new Random(); 
                int sampleSize = 8;
                
                // Create coordinate system.
                N = realHit.Normal;

                if (Math.Abs(N.X) > Math.Abs(N.Y))
                {
                    Nt = new Vector3(N.Z, 0, -N.X) / Math.Sqrt(N.X * N.X + N.Z * N.Z);
                } 
                else
                { 
                    Nt = new Vector3(0, -N.Z, N.Y) / Math.Sqrt(N.Y * N.Y + N.Z * N.Z);
                }
                Nt = Nt.Normalized(); 
                Nb = N.Cross(Nt).Normalized();

                double pdf = 1 / (2 * Math.PI);  
        
                for (int i = 0; i < sampleSize; i++)
                {   
                    double r1 = rand.NextDouble();
                    double r2 = rand.NextDouble();
                    Vector3 sample = uniformSampleHemisphere(r1, r2);
                    Vector3 sampleWorld = new Vector3( 
                    sample.X * Nb.X + sample.Y * N.X + sample.Z * Nt.X, 
                    sample.X * Nb.Y + sample.Y * N.Y + sample.Z * Nt.Y, 
                    sample.X * Nb.Z + sample.Y * N.Z + sample.Z * Nt.Z); 

                    newOrigin = outside ? realHit.Position + offset : realHit.Position - offset;  
                    newDirection = sampleWorld.Normalized();
                    newRay = new Ray(newOrigin, newDirection);
                    Color sampleColor = RayTrace(newRay, recurseTime + 1, recurseLimit) / pdf;
                    inDirectLight += sampleColor;   
                }

                inDirectLight /= sampleSize;

                pixelColor = (directLight + inDirectLight) * realEntity.Material.Color / Math.PI;

                return pixelColor;
            }

            // Reflection        
            if (realEntity.Material.Type == Material.MaterialType.Reflective)
            {   
                newOrigin = outside ? realHit.Position + offset : realHit.Position - offset;                     
                // R = I − 2 * (N dot I) * N
                newDirection = (realHit.Incident - 2 * (realHit.Normal.Dot(realHit.Incident)) * realHit.Normal).Normalized();
                newRay = new Ray(newOrigin, newDirection);
                pixelColor = RayTrace(newRay, recurseTime + 1, recurseLimit); 
                return pixelColor;
            }
                    
            //Refraction
            if (realEntity.Material.Type == Material.MaterialType.Refractive)
            {   
                // The Fresnel effect with reflective ray.                        
                // Fresnel ratio
                Vector3 n = realHit.Normal;
                double cosi = Math.Clamp(realHit.Incident.Dot(n), -1, 1);
                double etai = 1;
                double etat = realEntity.Material.RefractiveIndex;

                // outside the surface
                if (cosi < 0)
                {   
                    cosi = -cosi;
                }
                else
                {   
                    double temp = etai;
                    etai = etat;
                    etat = temp;
                    n = -realHit.Normal;
                }

                double eta = etai / etat;
                double k = 1 - eta * eta * (1 - cosi * cosi);
                double sint = etai / etat * Math.Sqrt(Math.Max(0, 1 - cosi * cosi));

                if (sint >= 1) 
                {
                    reflectRatio = 1; 
                }
                else
                {   
                    cosi = Math.Abs(cosi);
                    double cost = Math.Sqrt(Math.Max(0, 1 - sint * sint));
                    double rs = ((etat * cosi) - (etai * cost)) / ((etat * cosi) + (etai * cost));
                    double rp = ((etai * cosi) - (etat * cost)) / ((etai * cosi) + (etat * cost));
                    reflectRatio = (rs * rs + rp * rp) / 2;
                }

                if (reflectRatio < 1) {
                    // Refractive ray.
                    newOrigin = outside ? realHit.Position - offset : realHit.Position + offset;
                    newDirection = (eta * realHit.Incident + (eta * cosi - Math.Sqrt(k)) * n).Normalized();
                    newRay = new Ray(newOrigin, newDirection);
                    refractColor = RayTrace(newRay, recurseTime + 1, recurseLimit); 
                }
                    
                // Reflective ray.
                newOrigin = outside ? realHit.Position + offset : realHit.Position - offset;
                newDirection = (realHit.Incident - 2 * (realHit.Normal.Dot(realHit.Incident)) * realHit.Normal).Normalized();
                newRay = new Ray(newOrigin, newDirection);
                reflectColor = RayTrace(newRay, recurseTime + 1, recurseLimit);            
                pixelColor = refractColor * (1 - reflectRatio) + reflectColor * (reflectRatio);
                return pixelColor;
            }

            return pixelColor;   
        }

        // Handle RGB color when it overflow or underflow. 
        private Color NormalizedColor(Color color)
        {
            if (color.R > 1) color = new Color(1, color.G, color.B);
            if (color.G > 1) color = new Color(color.R, 1, color.B);
            if (color.B > 1) color = new Color(color.R, color.G, 1);
            if (color.R < 0) color = new Color(0, color.G, color.B);
            if (color.G < 0) color = new Color(color.R, 0, color.B);
            if (color.B < 0) color = new Color(color.R, color.G, 0);

            return color;
        }

        // Check whether the position of the hit is in shadow. 
        private Boolean InShadow(SceneEntity frontEntity, PointLight light, RayHit frontHit) 
        {   
            Vector3 position = frontHit.Position;
            
            Ray lightRay = new Ray(position, (light.Position - position).Normalized());
            
            foreach (SceneEntity entity in this.entities)
            {   
                RayHit lightHit = entity.Intersect(lightRay);

                if (lightHit != null) 
                {   
                    double distLight = (light.Position - frontHit.Position).LengthSq();
                    double distEntity = (lightHit.Position - frontHit.Position).LengthSq();
                    
                    if (distLight > distEntity && entity != frontEntity)
                    {
                        return true;
                    }
                }
            }
            return false; 
        }
        
        // For the using of ambient light mode.
        private Vector3 uniformSampleHemisphere(double r1, double r2) 
        {
            double sinTheta = Math.Sqrt(1 - r1 * r1); 
            double phi = 2 * Math.PI * r2; 
            double x = sinTheta * Math.Cos(phi); 
            double z = sinTheta * Math.Sin(phi); 
            return new Vector3(x, r1, z); 
        }
    }
}
