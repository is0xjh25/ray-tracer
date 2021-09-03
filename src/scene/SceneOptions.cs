using System;

namespace RayTracer
{
    /// <summary>
    /// Immutable structure representing various scene options.
    /// You may not utilise all of these variables - this will depend
    /// on which add-ons you choose to implement.
    /// </summary>
    public readonly struct SceneOptions
    {
        private readonly int aaMultiplier;
        private readonly bool ambientLightingEnabled;
        private readonly Vector3 cameraPosition, cameraAxis;
        private readonly double cameraAngle;
        private readonly double apertureRadius, focalLength;

        /// <summary>
        /// Construct scene options object.
        /// </summary>
        /// <param name="aaMultiplier">Anti-aliasing multiplier</param>
        /// <param name="ambientLightingEnabled">Flag to enable amibent lighting</param>
        /// <param name="cameraPosition">Camera position</param>
        /// <param name="cameraAxis">Camera rotation axis</param>
        /// <param name="cameraAngle">Camera rotation angle</param>
        /// <param name="apertureRadius">Physical camera aperture radius</param>
        /// <param name="focalLength">Physical camera focal length</param>
        public SceneOptions(
            int aaMultiplier,
            bool ambientLightingEnabled,
            Vector3 cameraPosition,
            Vector3 cameraAxis,
            double cameraAngle,
            double apertureRadius,
            double focalLength)
        {
            this.aaMultiplier = aaMultiplier;
            this.ambientLightingEnabled = ambientLightingEnabled;
            this.cameraPosition = cameraPosition;
            this.cameraAxis = cameraAxis;
            this.cameraAngle = cameraAngle;
            this.apertureRadius = apertureRadius;
            this.focalLength = focalLength;
        }

        /// <summary>
        /// Anti-aliasing multiplier. Specifies how many samples per pixel in 
        /// each axis. e.g. 2 => 4 samples, 3 => 9 samples, etc.
        /// </summary>
        public int AAMultiplier { get { return this.aaMultiplier; } }

        /// <summary>
        /// Whether ambient lighting computation should be enabled in the scene.
        /// </summary>
        public bool AmbientLightingEnabled { get { return this.ambientLightingEnabled; } }

        /// <summary>
        /// Camera position in the scene.
        /// </summary>
        public Vector3 CameraPosition { get { return this.cameraPosition; } }

        /// <summary>
        /// Camera rotation axis to specify orientation.
        /// </summary>
        public Vector3 CameraAxis { get { return this.cameraAxis; } }

        /// <summary>
        /// Camera rotation angle (lefthand-clockwise around rotation axis).
        /// </summary>
        public double CameraAngle { get { return this.cameraAngle; } }

        /// <summary>
        /// Aperture radius for simulating physical camera depth of field effects.
        /// </summary>
        public double ApertureRadius { get { return this.apertureRadius; } }

        /// <summary>
        /// Focal length for simulating physical camera depth of field effects.
        /// </summary>
        public double FocalLength { get { return this.focalLength; } }
    }
}
