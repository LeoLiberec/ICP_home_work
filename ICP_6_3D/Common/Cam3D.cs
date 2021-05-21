using System;
using OpenTK.Mathematics;

namespace ICP_6_3D.Common
{
    public class Cam3D
    {
        // Vector aby zjistit jak toci kamera
        private Vector3 front = -Vector3.UnitZ;

        private Vector3 up = Vector3.UnitY;

        private Vector3 right = Vector3.UnitX;

        // Tocime kolem X
        private float pitch;

        // Tocime kolem Y
        private float yaw = -MathHelper.PiOver2; 

        // Zorne pole
        private float fov = MathHelper.PiOver2;

        public Cam3D(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        // Pos 
        public Vector3 Position { get; set; }

        // AspectRatio okna pro matice
        public float AspectRatio { private get; set; }

        public Vector3 Front => front;

        public Vector3 Up => up;

        public Vector3 Right => right;

        // Convert stup => rad
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(pitch);
            set
            {
                // Omezeni kamery aby nepretocila
                var angle = MathHelper.Clamp(value, -89f, 89f);
                pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(yaw);
            set
            {
                yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov // Zorni pole
        {
            get => MathHelper.RadiansToDegrees(fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 40f);  // uhel 
                fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + front, up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            front.X = MathF.Cos(pitch) * MathF.Cos(yaw);
            front.Y = MathF.Sin(pitch);
            front.Z = MathF.Cos(pitch) * MathF.Sin(yaw);

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }
    }
}