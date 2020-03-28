using System.Numerics;

namespace ZEngine.Engine.Game
{
    public class Transform
    {
        private Matrix4x4 Matrix { get; set; }

        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public Vector2 Origin;
    }
}