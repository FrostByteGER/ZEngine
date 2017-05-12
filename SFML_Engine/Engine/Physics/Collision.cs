using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.Physics
{
	class Collision
	{
		public bool PointVSBox(Vector2f p, Vector2f box, Vector2f size)
		{
			if (box.X < p.X &&
				box.X + size.X > p.X &&
				box.Y < p.Y &&
				box.Y + size.Y > p.Y)
			{
				return true;
			}
			return false;
		}

		public bool PointVSSphere(Vector2f p, Vector2f Sphere, float r)
		{
			Vector2f dis = p - Sphere;

			if (Math.Pow((r), 2) < Math.Pow(dis.X, 2) + Math.Pow(dis.Y, 2))
			{
				return true;
			}
			return false;
		}

		public bool EdgeVSSphere(Vector2f ep1, Vector2f ep2, Vector2f sp, float r)
		{

			// TODO

			Vector2f mid = (ep1 + ep2)/2;
			Vector2f ervec = (ep1 - ep2)/2;

			Vector2f dis = sp - mid;

			if (Math.Pow((r + er), 2) < Math.Pow(dis.X, 2) + Math.Pow(dis.Y, 2))
			{

			}

			return false;
		}

		public bool BoxVSBox(Vector2f p1, Vector2f size1, Vector2f p2, Vector2f size2)
		{
			if (p1.X < p2.X + size2.X &&
				p1.X + size1.X > p2.X &&
				p1.Y < p2.Y + size2.Y &&
				p1.Y + size1.Y > p2.Y)
			{
				return true;
			}
			return false;
		}

		public bool SphereVSSphere(Vector2f p1, float r1, Vector2f p2, float r2)
		{
			Vector2f dis = p1 - p2;

			if (Math.Pow((r1 + r2), 2) < Math.Pow(dis.X, 2) + Math.Pow(dis.Y, 2))
			{
				return true;
			}
			return false;
		}

	}
}
