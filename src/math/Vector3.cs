using System;

namespace RayTracer
{
    /// <summary>
    /// Immutable structure to represent a three-dimensional vector.
    /// </summary>
    public readonly struct Vector3
    {
        private readonly double x, y, z;

        /// <summary>
        /// Construct a three-dimensional vector.
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Convert vector to a readable string.
        /// </summary>
        /// <returns>Vector as string in form (x, y, z)</returns>
        public override string ToString()
        {
            return "(" + this.x + "," + this.y + "," + this.z + ")";
        }

        /// <summary>
        /// Compute the length of the vector squared.
        /// This should be used if there is a way to perform a vector
        /// computation without needing the actual length, since
        /// a square root operation is expensive.
        /// </summary>
        /// <returns>Length of the vector squared</returns>
        public double LengthSq()
        {
            double newX = Math.Pow(this.x, 2);
            double newY = Math.Pow(this.y, 2);
            double newZ = Math.Pow(this.z, 2);
            double sum = newX + newY + newZ;
            
            return  sum;
        }

        /// <summary>
        /// Compute the length of the vector.
        /// </summary>
        /// <returns>Length of the vector</returns>
        public double Length()
        {
            return Math.Sqrt(LengthSq());
        }

        /// <summary>
        /// Compute a length 1 vector in the same direction.
        /// </summary>
        /// <returns>Normalized vector</returns>
        public Vector3 Normalized()
        {
            double newX = this.x / Length();
            double newY = this.y / Length();
            double newZ = this.z / Length();
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Compute the dot product with another vector.
        /// </summary>
        /// <param name="with">Vector to dot product with</param>
        /// <returns>Dot product result</returns>
        public double Dot(Vector3 with)
        {
            double diffX = this.x * with.x;
            double diffY = this.y * with.y;
            double diffZ = this.z * with.z;
            double dot = diffX + diffY + diffZ;
            
            return dot;
        }

        /// <summary>
        /// Compute the cross product with another vector.
        /// </summary>
        /// <param name="with">Vector to cross product with</param>
        /// <returns>Cross product result</returns>
        public Vector3 Cross(Vector3 with)
        {
            double newX = this.y * with.z - this.z * with.y;
            double newY = this.z * with.x - this.x * with.z;
            double newZ = this.x * with.y - this.y * with.x;

            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Sum two vectors together (using + operator).
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Summed vector</returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            double newX = a.x + b.x;
            double newY = a.y + b.y;
            double newZ = a.z + b.z;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Negate a vector (using - operator)
        /// </summary>
        /// <param name="a">Vector to negate</param>
        /// <returns>Negated vector</returns>
        public static Vector3 operator -(Vector3 a)
        {
            double newX = -a.x;
            double newY = -a.y;
            double newZ = -a.z;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Subtract one vector from another.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Vector to subtract</param>
        /// <returns>Subtracted vector</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            double newX = a.x - b.x;
            double newY = a.y - b.y;
            double newZ = a.z - b.z;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Multiply a vector by a scalar value.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Scalar multiplier</param>
        /// <returns>Multiplied vector</returns>
        public static Vector3 operator *(Vector3 a, double b)
        {
            double newX = a.x * b;
            double newY = a.y * b;
            double newZ = a.z * b;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Multiply a vector by a scalar value (opposite operands).
        /// </summary>
        /// <param name="b">Scalar multiplier</param>
        /// <param name="a">Original vector</param>
        /// <returns>Multiplied vector</returns>
        public static Vector3 operator *(double b, Vector3 a)
        {
            double newX = a.x * b;
            double newY = a.y * b;
            double newZ = a.z * b;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// Divide a vector by a scalar value.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Scalar divisor</param>
        /// <returns>Divided vector</returns>
        public static Vector3 operator /(Vector3 a, double b)
        {
            double newX = a.x / b;
            double newY = a.y / b;
            double newZ = a.z / b;
            
            return new Vector3(newX, newY, newZ);
        }

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double X { get { return this.x; } }

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public double Y { get { return this.y; } }

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public double Z { get { return this.z; } }
    }
}
