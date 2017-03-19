using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Game
{
    public interface ITickableInterface
    {
        void Tick(uint deltaTime);
    }
}
