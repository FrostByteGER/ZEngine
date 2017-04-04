using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly Vector2f Gravity = new Vector2f(0.0f ,9.81f);
		public bool GlobalGravityEnabled{ get; set; } = false;

        private Dictionary<string, List<Actor>> ActorGroups = new Dictionary<string, List<Actor>>();

		private Dictionary<string, List<string>> CollidablePartner = new Dictionary<string, List<string>>();

		private Dictionary<string, List<string>> OverlapPartner = new Dictionary<string, List<string>>();


		//var accounts = new Dictionary<string, double>();

		internal void PhysicsTick(float deltaTime, ref List<Actor> actors)
        {
			// move Actors

			foreach (Actor actor in actors)
			{
				if (actor != null)
				{
					if (actor.Movable)
					{
						actor.Move(actor.Position += actor.Velocity * deltaTime);

						if (GlobalGravityEnabled)
						{
							actor.Velocity = actor.Velocity + (Gravity + actor.Acceleration)*deltaTime;
						}
						else
						{
							actor.Velocity = actor.Velocity + actor.Acceleration * deltaTime;
						}
					}
				}
			}

			//Collision / Overlap

			foreach (string groupNameActive in ActorGroups.Keys )
			{
				if (OverlapPartner.ContainsKey(groupNameActive))
				{
					foreach (string groupNamePassive in OverlapPartner[groupNameActive])
					{
						foreach (Actor activeActor in ActorGroups[groupNameActive])
						{
							foreach (Actor passiveActor in ActorGroups[groupNamePassive])
							{
								//Box/Shere
								if (passiveActor.CollisionShape.GetType() != activeActor.CollisionShape.GetType())
								{
									BoxShape box;
									SphereShape sphere;

									Actor boxActor;
									Actor sphereActor;

									if (passiveActor.CollisionShape.GetType() == typeof(BoxShape))
									{
										box = (BoxShape)passiveActor.CollisionShape;
										sphere = (SphereShape)activeActor.CollisionShape;

										boxActor = passiveActor;
										sphereActor = activeActor;
									}
									else
									{
										box = (BoxShape)activeActor.CollisionShape;
										sphere = (SphereShape)passiveActor.CollisionShape;

										boxActor = activeActor;
										sphereActor = passiveActor;
									}

									double distanceX = Math.Min(Math.Abs(boxActor.Position.X - sphere.getMid(sphereActor.Position).X), Math.Abs(boxActor.Position.X + box.BoxExtent.X - sphere.getMid(sphereActor.Position).X));
									double distanceY = Math.Min(Math.Abs(boxActor.Position.Y - sphere.getMid(sphereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.BoxExtent.X - sphere.getMid(sphereActor.Position).Y));

									//bounce from eage
									if (sphere.SphereRadius * sphere.SphereRadius > distanceX * distanceX + distanceY * distanceY)
									{

										double normTempAdd = distanceX + distanceY;
										double normTempOld = sphereActor.Velocity.X + sphereActor.Velocity.Y;

										// temp
										sphereActor.Velocity = new Vector2f((float)((sphereActor.Velocity.X / normTempOld) + (distanceX / normTempAdd)) / 2, (float)((sphereActor.Velocity.Y / normTempOld) + (distanceY / normTempAdd)) / 2);

									}//right
									else if (boxActor.Position.X < sphere.getMid(sphereActor.Position).X + sphere.SphereRadius && boxActor.Position.X > sphere.getMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{
										activeActor.IsOverlaping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												sphereActor.Velocity = new Vector2f(-sphereActor.Velocity.X, sphereActor.Velocity.Y);

												sphereActor.Acceleration = new Vector2f(-sphereActor.Acceleration.X, sphereActor.Acceleration.Y);

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//left
									else if (boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X - sphere.SphereRadius && boxActor.Position.X + box.BoxExtent.X < sphere.getMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{
										activeActor.IsOverlaping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												sphereActor.Velocity = new Vector2f(-sphereActor.Velocity.X, sphereActor.Velocity.Y);

												sphereActor.Acceleration = new Vector2f(-sphereActor.Acceleration.X, sphereActor.Acceleration.Y);

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//top
									else if (boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y + sphere.SphereRadius && boxActor.Position.Y > sphere.getMid(sphereActor.Position).Y &&
										boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X)
									{
										activeActor.IsOverlaping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, -sphereActor.Velocity.Y);

												sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, -sphereActor.Acceleration.Y);

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//bottem
									else if (boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y - sphere.SphereRadius && boxActor.Position.Y + box.BoxExtent.Y < sphere.getMid(sphereActor.Position).Y &&
										boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X)
									{
										activeActor.IsOverlaping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, -sphereActor.Velocity.Y);

												sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, -sphereActor.Acceleration.Y);

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//isInside
									else if (boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{
										activeActor.IsOverlaping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												//TODO Shere is inside Box

												activeActor.AfterCollision(passiveActor);

											}
										}
									}	
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
											activeActor.IsOverlaping(passiveActor);

											if (CollidablePartner.ContainsKey(groupNameActive))
											{
												if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
												{
													activeActor.BeforeCollision(passiveActor);

													//TODO 

													activeActor.AfterCollision(passiveActor);

												}
											}
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
										double distance = Math.Pow((activeTemp.Position.X + activeTemp.SphereRadius) - (passiveTemp.Position.X + passiveTemp.SphereRadius), 2f) +
														Math.Pow((activeTemp.Position.Y + activeTemp.SphereRadius) - (passiveTemp.Position.Y + passiveTemp.SphereRadius), 2f);

										if (distance < (activeTemp.SphereRadius * activeTemp.SphereRadius) + (passiveTemp.SphereRadius * passiveTemp.SphereRadius))
										{
											activeActor.IsOverlaping(passiveActor);

											if (CollidablePartner.ContainsKey(groupNameActive))
											{
												if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
												{
													activeActor.BeforeCollision(passiveActor);

													//TODO 

													activeActor.AfterCollision(passiveActor);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		//ActerGroup

		public void AddActorToGroup(string groupName, Actor actor)
		{
			if (ActorGroups.ContainsKey(groupName)) {
				ActorGroups[groupName].Add(actor);
			}
			else
			{
				ActorGroups.Add(groupName,new List<Actor>());
				ActorGroups[groupName].Add(actor);
			}
		}

		public bool RemoveActorFromGroup(string groupName, Actor actor)
		{
			if (ActorGroups.ContainsKey(groupName))
			{
				return ActorGroups[groupName].Remove(actor);
			}
			return false;
		}

		public bool AddGroup(string groupName)
		{
			if (!ActorGroups.ContainsKey(groupName))
			{
				ActorGroups.Add(groupName, new List<Actor>());
				return true;
			}

			return false;
		}

		public bool RemoveGroup(string groupName, bool save = true)
		{
			if (ActorGroups.ContainsKey(groupName))
			{
				if (save)
				{
					if (ActorGroups[groupName].Count == 0)
					{
						return ActorGroups.Remove(groupName);
					}
					else
					{
						return false;
					}
				}
				else
				{
					return ActorGroups.Remove(groupName);
				}
			}
			return false;
		}

		// OverlapPartner

		public bool AddOverlapPartners(string activ, string passive)
		{
			if (ActorGroups.ContainsKey(activ) && ActorGroups.ContainsKey(passive))
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

		public bool RemoveOverlapPartners(string activ, string passive)
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
		public bool AddCollidablePartner(string activ, string passive)
		{
			if (ActorGroups.ContainsKey(activ) && ActorGroups.ContainsKey(passive))
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

				AddOverlapPartners(activ, passive);

				return true;
			}
			return false;
		}

		public bool RemoveCollidablePartner(string activ, string passive)
		{
			if (!CollidablePartner.ContainsKey(activ))
			{
				if (!CollidablePartner[activ].Contains(passive))
				{
					return true;
				}
				RemoveCollidablePartner(activ, passive);
				return CollidablePartner[activ].Remove(passive);
			}
			return true;
		}

		//Overlap not used
		private bool IsOverlapping(Actor activeActor, Actor passiveActor)
		{
			//Box/Shere
			if (passiveActor.CollisionShape.GetType() != activeActor.CollisionShape.GetType())
			{
				BoxShape box;
				SphereShape sphere;

				Actor boxActor;
				Actor sphereActor;

				if (passiveActor.CollisionShape.GetType() == typeof(BoxShape))
				{
					box = (BoxShape)passiveActor.CollisionShape;
					sphere = (SphereShape)activeActor.CollisionShape;

					boxActor = passiveActor;
					sphereActor = activeActor;
				}
				else
				{
					box = (BoxShape)activeActor.CollisionShape;
					sphere = (SphereShape)passiveActor.CollisionShape;

					boxActor = activeActor;
					sphereActor = passiveActor;
				}

				double distanceX = Math.Min(Math.Abs(boxActor.Position.X - sphere.getMid(sphereActor.Position).X), Math.Abs(boxActor.Position.X + box.BoxExtent.X - sphere.getMid(sphereActor.Position).X));
				double distanceY = Math.Min(Math.Abs(boxActor.Position.Y - sphere.getMid(sphereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.BoxExtent.X - sphere.getMid(sphereActor.Position).Y));

				if (sphere.SphereRadius * sphere.SphereRadius > distanceX * distanceX + distanceY * distanceY)
				{
					return true;
				}
				else if ((boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.Y > sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y < sphere.getMid(sphereActor.Position).Y) ||
						(boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X && boxActor.Position.Y > sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y < sphere.getMid(sphereActor.Position).Y) ||
													(boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.X > sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X < sphere.getMid(sphereActor.Position).X) ||
						(boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y && boxActor.Position.X > sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X < sphere.getMid(sphereActor.Position).X)
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