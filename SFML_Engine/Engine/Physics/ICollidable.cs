﻿
namespace SFML_Engine.Engine.Physics
{
	public interface ICollidable
	{

		bool CollisionCallbacksEnabled { get; set; }
		void OnCollide();

	}
}