using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine
{
    public interface IMovable
    {
        void SetMovable(bool movable);
        bool GetMovable();
        void SetVelocity(Vector2f velocity);
        Vector2f GetVelocity();
        void SetAcceleration(Vector2f acceleration);
        Vector2f GetAcceleration();
    }
}
