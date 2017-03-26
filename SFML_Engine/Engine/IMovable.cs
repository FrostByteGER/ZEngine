using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public interface IMovable
    {

        bool Movable { get; set; }
        Vector2f Velocity { get; set; }
        Vector2f Acceleration { get; set; }

    }
}
