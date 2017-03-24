using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine
{
    public interface ICollidable
    {
        Shape CollisionShape { get; set; }

    }
}
