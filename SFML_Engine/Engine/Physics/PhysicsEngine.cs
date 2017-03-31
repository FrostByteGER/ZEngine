using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly float Gravity = 9.81f;
		public bool hasGravity = false;

        private Dictionary<string, List<Transformable>> ActerGroups = new Dictionary<string, List<Transformable>>();

		private Dictionary<string, List<string>> CollidablePartner = new Dictionary<string, List<string>>();

		private Dictionary<string, List<string>> OverlapPartner = new Dictionary<string, List<string>>();


		//var accounts = new Dictionary<string, double>();

		internal void PhysicsTick(float deltaTime, ref List<Transformable> actors)
        {
            Console.WriteLine("Physics Tick");

			//Collision

			moveActors(deltaTime, ref actors);

			

        }

        private void moveActors(float deltaTime, ref List<Transformable> actors)
        {

            Vector2f VelocityTemp;

            foreach (var actor in actors)
            {
                var OneActor = actor as IActorable;
                //tickableActor?.Tick(deltaTime);

                if (OneActor != null)
                {
                    if (OneActor.Movable)
                    {
						OneActor.Move(OneActor.Velocity);

						//increase Velocity.x/.y by Acceleration.x/.y

						//OneActor.Move(x,y);
						if (hasGravity)
						{
							//addGravity
						}
                    }
                }
            }
        }

		public void addActorToGroup(string groupName, Transformable actor)
		{
			if (ActerGroups.ContainsKey(groupName)) {
				ActerGroups[groupName].Add(actor);
			}
			else
			{
				ActerGroups.Add(groupName,new List<Transformable>());
				ActerGroups[groupName].Add(actor);
			}
		}

		public bool subActorFromGroup(string groupName, Transformable actor)
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
				ActerGroups.Add(groupName, new List<Transformable>());
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

		//TODO
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

		private void overlapActor()
		{

		}

		private void collidActor()
		{
			
		}
	}
}