using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly Vector2f Gravity = new Vector2f(0.0f ,9.81f);
		public bool hasGravity = false;

        private Dictionary<string, List<Actor>> ActerGroups = new Dictionary<string, List<Actor>>();

		private Dictionary<string, List<string>> CollidablePartner = new Dictionary<string, List<string>>();

		private Dictionary<string, List<string>> OverlapPartner = new Dictionary<string, List<string>>();


		//var accounts = new Dictionary<string, double>();

		internal void PhysicsTick(float deltaTime, ref List<Actor> actors)
        {
			//Console.WriteLine("Physics Tick"); Debug

			// move Actors

			//Vector2f VelocityTemp;

			foreach (Actor actor in actors)
			{
				//var OneActor = actor as IActorable;

				if (actor != null)
				{
					if (actor.Movable)
					{
						actor.Move(actor.Position += actor.Velocity * deltaTime);

						if (hasGravity)
						{
							actor.Velocity = actor.Velocity + (Gravity + actor.Acceleration)*deltaTime;
						}
					}
				}
			}

			//Collision / Overlap

			foreach (string groupNameActive in ActerGroups.Keys )
			{
				if (OverlapPartner.ContainsKey(groupNameActive))
				{
					foreach (string groupNamePassive in OverlapPartner[groupNameActive])
					{

						foreach (Actor activeActor in ActerGroups[groupNameActive])
						{
							foreach (Actor passiveActor in ActerGroups[groupNamePassive])
							{

								//TODO Colliding
								if (isOverlaping(activeActor, passiveActor))
								{

								}
							}
						}
					}
				}
			}
		}

		//ActerGroup

		public void addActorToGroup(string groupName, Actor actor)
		{
			if (ActerGroups.ContainsKey(groupName)) {
				ActerGroups[groupName].Add(actor);
			}
			else
			{
				ActerGroups.Add(groupName,new List<Actor>());
				ActerGroups[groupName].Add(actor);
			}
		}

		public bool subActorFromGroup(string groupName, Actor actor)
		{
			if (ActerGroups.ContainsKey(groupName))
			{
				return ActerGroups[groupName].Remove(actor);
			}
			return false;
		}

		public bool addGroup(string groupName)
		{
			if (!ActerGroups.ContainsKey(groupName))
			{
				ActerGroups.Add(groupName, new List<Actor>());
				return true;
			}

			return false;
		}

		public bool subGroup(string groupName, bool save = true)
		{
			if (ActerGroups.ContainsKey(groupName))
			{
				if (save)
				{
					if (ActerGroups[groupName].Count == 0)
					{
						return ActerGroups.Remove(groupName);
					}
					else
					{
						return false;
					}
				}
				else
				{
					return ActerGroups.Remove(groupName);
				}
			}
			return false;
		}

		// OverlapPartner

		public bool addOverlapPartners(string activ, string passive)
		{
			if (ActerGroups.ContainsKey(activ) && ActerGroups.ContainsKey(passive))
			{
				if (!OverlapPartner.ContainsKey(activ))
				{
					OverlapPartner.Add(activ, new List<string>());
					OverlapPartner[activ].Add(passive);
				}else if (!OverlapPartner[activ].Contains(passive))
				{
					OverlapPartner[activ].Add(passive);
				}
				return true;
			}
			return false;
		}

		public bool subOverlapPartners(string activ, string passive)
		{
			if (!OverlapPartner.ContainsKey(activ))
			{
				if (!OverlapPartner[activ].Contains(passive))
				{
					return true;
				}
				return OverlapPartner[activ].Remove(passive);			
			}
			return true;
		}

		//CollidPartner

		public bool addCollidPartner(string activ, string passive)
		{
			if (ActerGroups.ContainsKey(activ) && ActerGroups.ContainsKey(passive))
			{
				if (!CollidablePartner.ContainsKey(activ))
				{
					CollidablePartner.Add(activ, new List<string>());
					CollidablePartner[activ].Add(passive);
				}
				else if (!CollidablePartner[activ].Contains(passive))
				{
					CollidablePartner[activ].Add(passive);
				}
				return true;
			}
			return false;
		}

		public bool subCollidPartner(string activ, string passive)
		{
			if (!CollidablePartner.ContainsKey(activ))
			{
				if (!CollidablePartner[activ].Contains(passive))
				{
					return true;
				}
				return CollidablePartner[activ].Remove(passive);
			}
			return true;
		}

		//Overlap
		private bool isOverlaping(Actor activeActor, Actor passiveActor)
		{
			//Box/Shere
			if (passiveActor.CollisionShape.GetType() != activeActor.CollisionShape.GetType())
			{
				BoxShape box;
				SphereShape shere;

				Actor boxActor;
				Actor shereActor;

				if (passiveActor.CollisionShape.GetType() == typeof(BoxShape))
				{
					box = (BoxShape)passiveActor.CollisionShape;
					shere = (SphereShape)activeActor.CollisionShape;

					boxActor = passiveActor;
					shereActor = activeActor;
				}
				else
				{
					box = (BoxShape)activeActor.CollisionShape;
					shere = (SphereShape)passiveActor.CollisionShape;

					boxActor = activeActor;
					shereActor = passiveActor;
				}

				double distanceX = Math.Min(Math.Abs(boxActor.Position.X - shere.getMid(shereActor.Position).X), Math.Abs(boxActor.Position.X + box.BoxExtent.X - shere.getMid(shereActor.Position).X));
				double distanceY = Math.Min(Math.Abs(boxActor.Position.Y - shere.getMid(shereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.BoxExtent.X - shere.getMid(shereActor.Position).Y));

				if (shere.SphereRadius * shere.SphereRadius > distanceX * distanceX + distanceY * distanceY)
				{
					return true;
				}
				else if ((boxActor.Position.X < shere.getMid(shereActor.Position).X && boxActor.Position.Y > shere.getMid(shereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y < shere.getMid(shereActor.Position).Y) ||
						(boxActor.Position.X + box.BoxExtent.X > shere.getMid(shereActor.Position).X && boxActor.Position.Y > shere.getMid(shereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y < shere.getMid(shereActor.Position).Y) ||
													(boxActor.Position.Y < shere.getMid(shereActor.Position).Y && boxActor.Position.X > shere.getMid(shereActor.Position).X && boxActor.Position.X + box.BoxExtent.X < shere.getMid(shereActor.Position).X) ||
						(boxActor.Position.Y + box.BoxExtent.Y > shere.getMid(shereActor.Position).Y && boxActor.Position.X > shere.getMid(shereActor.Position).X && boxActor.Position.X + box.BoxExtent.X < shere.getMid(shereActor.Position).X)
						)
				{
					return true;
				}
				return false;
			}

			//Box/Box
			if (activeActor.CollisionShape.GetType() == typeof(BoxShape))
			{
				BoxShape activeTemp = (BoxShape)activeActor.CollisionShape;

				if (passiveActor.CollisionShape.GetType() == typeof(BoxShape))
				{
					BoxShape passiveTemp = (BoxShape)passiveActor.CollisionShape;

					if (activeTemp.Position.X < passiveTemp.Position.X + passiveTemp.BoxExtent.X &&
						activeTemp.Position.X + activeTemp.BoxExtent.X > passiveTemp.Position.X &&
						activeTemp.Position.Y < passiveTemp.Position.Y + passiveTemp.BoxExtent.Y &&
						activeTemp.Position.Y + activeTemp.BoxExtent.Y > passiveTemp.Position.Y
						)
					{
						return true;
					}
				}
			}//Shere/Shere
			else if (activeActor.CollisionShape.GetType() == typeof(SphereShape))
			{
				SphereShape activeTemp = (SphereShape)activeActor.CollisionShape;
				if (passiveActor.CollisionShape.GetType() == typeof(BoxShape))
				{
					SphereShape passiveTemp = (SphereShape)passiveActor.CollisionShape;

					// distance^2
					double distance = Math.Pow((activeTemp.Position.X + activeTemp.SphereRadius ) - (passiveTemp.Position.X + passiveTemp.SphereRadius) , 2f)+
									Math.Pow((activeTemp.Position.Y + activeTemp.SphereRadius ) - (passiveTemp.Position.Y + passiveTemp.SphereRadius), 2f);

					if (distance < (activeTemp.SphereRadius*activeTemp.SphereRadius) + (passiveTemp.SphereRadius*passiveTemp.SphereRadius))
					{
						return true;
					}
				}
			}
			return false;
		}

		//Collid

		//extra

		private Vector2f addVectorfToVectorf(Vector2f v1, Vector2f v2)
		{
			return v1 + v2;
		}
	}
}