using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class PhysicsEngine
    {

        public readonly Vector2f Gravity = new Vector2f(0.0f, 9.81f);
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

						if (GlobalGravityEnabled && actor.HasGravity)
						{
							actor.Velocity = (actor.Velocity + (Gravity + actor.Acceleration)*deltaTime) * (1-actor.Friction);
						}
						else
						{
							actor.Velocity = (actor.Velocity + actor.Acceleration * deltaTime) * (1 - actor.Friction);
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
							if (activeActor.MarkedForRemoval)
							{
								continue;
							}
							foreach (Actor passiveActor in ActorGroups[groupNamePassive])
							{

								// If both actors are one and the same or the passive one is marked for removal, then continue
								if (activeActor.Equals(passiveActor) || passiveActor.MarkedForRemoval)
								{
									continue;
								}

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
									double distanceY = Math.Min(Math.Abs(boxActor.Position.Y - sphere.getMid(sphereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.BoxExtent.Y - sphere.getMid(sphereActor.Position).Y));

									//bounce from eage
									if ((sphere.SphereDiameter/2) * (sphere.SphereDiameter / 2) > distanceX * distanceX + distanceY * distanceY)
									{
										
										double normTempX = distanceX / (distanceX + distanceY);
										double normTempY = distanceY / (distanceX + distanceY);

										//dumb Temp
										if (boxActor.Position.X + box.BoxExtent.X/2f > sphere.getMid(sphereActor.Position).X)
										{
											normTempX *= -1;
										}
										if (boxActor.Position.Y + box.BoxExtent.Y/2f > sphere.getMid(sphereActor.Position).Y)
										{
											normTempY *= -1;
										}

										float sphereSpeed = Math.Abs(sphereActor.Velocity.X) + Math.Abs(sphereActor.Velocity.Y) + Math.Abs(boxActor.Velocity.X) + Math.Abs(boxActor.Velocity.Y);

										sphereActor.Velocity = new Vector2f((float)normTempX*sphereSpeed, (float)normTempY*sphereSpeed);

									}//box comes from right 
									if (boxActor.Position.X < sphereActor.Position.X + sphere.SphereDiameter && boxActor.Position.X > sphereActor.Position.X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{

										activeActor.IsOverlapping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);
												if (sphereActor.Velocity.X > 0)
												{
													sphereActor.Velocity = new Vector2f(-sphereActor.Velocity.X + boxActor.Velocity.X, sphereActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(-sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}
												else
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X + boxActor.Velocity.X, sphereActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//box comes from left
									else if (boxActor.Position.X + box.BoxExtent.X > sphereActor.Position.X && boxActor.Position.X + box.BoxExtent.X < sphere.getMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{
										activeActor.IsOverlapping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												if (sphereActor.Velocity.X < 0)
												{
													sphereActor.Velocity = new Vector2f(-sphereActor.Velocity.X + boxActor.Velocity.X, sphereActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(-sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}
												else
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X + boxActor.Velocity.X, sphereActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//box comes from bottem
									else if (boxActor.Position.Y < sphereActor.Position.Y + sphere.SphereDiameter && boxActor.Position.Y > sphereActor.Position.Y &&
										boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X)
									{
										activeActor.IsOverlapping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												if (sphereActor.Velocity.Y > 0)
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, -sphereActor.Velocity.Y + boxActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, -sphereActor.Acceleration.Y);
												}
												else
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, sphereActor.Velocity.Y + boxActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//box comes from top
									else if (boxActor.Position.Y + box.BoxExtent.Y > sphereActor.Position.Y && boxActor.Position.Y + box.BoxExtent.Y < sphere.getMid(sphereActor.Position).Y &&
										boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X)
									{
										activeActor.IsOverlapping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												if (sphereActor.Velocity.Y < 0)
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, -sphereActor.Velocity.Y + boxActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, -sphereActor.Acceleration.Y);
												}
												else
												{
													sphereActor.Velocity = new Vector2f(sphereActor.Velocity.X, sphereActor.Velocity.Y + boxActor.Velocity.Y);

													sphereActor.Acceleration = new Vector2f(sphereActor.Acceleration.X, sphereActor.Acceleration.Y);
												}

												activeActor.AfterCollision(passiveActor);

											}
										}
									}//isInside
									else if (boxActor.Position.X < sphere.getMid(sphereActor.Position).X && boxActor.Position.X + box.BoxExtent.X > sphere.getMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.getMid(sphereActor.Position).Y && boxActor.Position.Y + box.BoxExtent.Y > sphere.getMid(sphereActor.Position).Y)
									{
										activeActor.IsOverlapping(passiveActor);

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

										if (activeActor.Position.X < passiveActor.Position.X + passiveTemp.BoxExtent.X &&
											activeActor.Position.X + activeTemp.BoxExtent.X > passiveActor.Position.X &&
											activeActor.Position.Y < passiveActor.Position.Y + passiveTemp.BoxExtent.Y &&
											activeTemp.BoxExtent.Y + activeActor.Position.Y > passiveActor.Position.Y
											)
										{
											activeActor.IsOverlapping(passiveActor);

											if (CollidablePartner.ContainsKey(groupNameActive))
											{
												if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
												{
													activeActor.BeforeCollision(passiveActor);
													//X
													if ((Math.Abs(activeTemp.getMid(activeActor.Position).X - passiveTemp.getMid(passiveActor.Position).X)) < (activeTemp.BoxExtent.X + passiveTemp.BoxExtent.X)/2f &&
															(Math.Abs(activeTemp.getMid(activeActor.Position).X - passiveTemp.getMid(passiveActor.Position).X)) > Math.Max(activeTemp.BoxExtent.X/2f,  passiveTemp.BoxExtent.X/2f))
													{
														if (activeActor.Movable)
														{
															if (passiveActor.Movable)
															{
																float temp = activeActor.Velocity.X;

																activeActor.Velocity = new Vector2f(passiveActor.Velocity.X*(activeActor.Mass/passiveActor.Mass), activeActor.Velocity.Y);
																passiveActor.Velocity = new Vector2f(temp* (passiveActor.Mass/activeActor.Mass), passiveActor.Velocity.Y);

																//TODO Acceleration
																if ((activeActor.Acceleration.X < 0 && activeActor.Velocity.X < 0) ||
																	(activeActor.Acceleration.X > 0 && activeActor.Velocity.X > 0))
																{
																	activeActor.Acceleration = new Vector2f(-activeActor.Acceleration.X, activeActor.Acceleration.Y);
																}

																activeActor.Velocity = new Vector2f(-activeActor.Velocity.X, activeActor.Velocity.Y);
																
															}
															else
															{

																if ((activeActor.Acceleration.X < 0 && activeActor.Velocity.X < 0) ||
																	(activeActor.Acceleration.X > 0 && activeActor.Velocity.X > 0))
																{
																	activeActor.Acceleration = new Vector2f(-activeActor.Acceleration.X, activeActor.Acceleration.Y);
																}

																activeActor.Velocity = new Vector2f(-activeActor.Velocity.X, activeActor.Velocity.Y);
															}
														}
														else
														{
															if (passiveActor.Movable)
															{

																if ((passiveActor.Acceleration.X < 0 && passiveActor.Velocity.X < 0) ||
																	(passiveActor.Acceleration.X > 0 && passiveActor.Velocity.X > 0))
																{
																	passiveActor.Acceleration = new Vector2f(-passiveActor.Acceleration.X, passiveActor.Acceleration.Y);
																}

																passiveActor.Velocity = new Vector2f(-passiveActor.Velocity.X, passiveActor.Velocity.Y);
															}
															else
															{
																activeActor.Velocity = new Vector2f(0, activeActor.Velocity.Y);
																passiveActor.Velocity = new Vector2f(0, passiveActor.Velocity.Y);

																activeActor.Acceleration = new Vector2f(0, activeActor.Acceleration.Y);
																passiveActor.Acceleration = new Vector2f(0, passiveActor.Acceleration.Y);
															}
														}
													}//Y
													if ((Math.Abs(activeTemp.getMid(activeActor.Position).Y - passiveTemp.getMid(passiveActor.Position).Y)) < (activeTemp.BoxExtent.Y + passiveTemp.BoxExtent.Y)/2f &&
														(Math.Abs(activeTemp.getMid(activeActor.Position).Y - passiveTemp.getMid(passiveActor.Position).Y)) > Math.Max(activeTemp.BoxExtent.Y/2f, passiveTemp.BoxExtent.Y/2f))
													{
														if (activeActor.Movable)
														{
															if (passiveActor.Movable)
															{
																float temp = activeActor.Velocity.Y;

																activeActor.Velocity = new Vector2f(activeActor.Velocity.X, passiveActor.Velocity.Y * (activeActor.Mass / passiveActor.Mass));
																passiveActor.Velocity = new Vector2f(passiveActor.Velocity.X, temp * (passiveActor.Mass / activeActor.Mass));

																//TODO Acceleration

																if ((activeActor.Acceleration.Y < 0 && activeActor.Velocity.Y < 0) ||
																	(activeActor.Acceleration.Y > 0 && activeActor.Velocity.Y > 0))
																{
																	activeActor.Acceleration = new Vector2f(activeActor.Acceleration.X, -activeActor.Acceleration.Y);
																}

																activeActor.Velocity = new Vector2f(activeActor.Velocity.X, -activeActor.Velocity.Y);

															}
															else
															{
																if ((activeActor.Acceleration.Y < 0 && activeActor.Velocity.Y < 0) ||
																	(activeActor.Acceleration.Y > 0 && activeActor.Velocity.Y > 0))
																{
																	activeActor.Acceleration = new Vector2f(activeActor.Acceleration.X, -activeActor.Acceleration.Y);
																}

																activeActor.Velocity = new Vector2f(activeActor.Velocity.X, -activeActor.Velocity.Y);
															}
														}
														else
														{
															if (passiveActor.Movable)
															{

																if ((passiveActor.Acceleration.Y < 0 && passiveActor.Velocity.Y < 0) ||
																	(passiveActor.Acceleration.Y > 0 && passiveActor.Velocity.Y > 0))
																{
																	passiveActor.Acceleration = new Vector2f(passiveActor.Acceleration.X, -passiveActor.Acceleration.Y);
																}

																passiveActor.Velocity = new Vector2f(activeActor.Velocity.X, -activeActor.Velocity.Y);

															}
															else
															{
																activeActor.Velocity = new Vector2f(activeActor.Velocity.X ,0);
																passiveActor.Velocity = new Vector2f(passiveActor.Velocity.X, 0);

																activeActor.Acceleration = new Vector2f(activeActor.Acceleration.X, 0);
																passiveActor.Acceleration = new Vector2f(passiveActor.Acceleration.X, 0);
															}
														}
													}
													activeActor.AfterCollision(passiveActor);
												}
											}
										}
									}
								}//Shere/Shere
								else if (activeActor.CollisionShape.GetType() == typeof(SphereShape))
								{
									SphereShape activeTemp = (SphereShape)activeActor.CollisionShape;
									if (passiveActor.CollisionShape.GetType() == typeof(SphereShape))
									{
										SphereShape passiveTemp = (SphereShape)passiveActor.CollisionShape;

										double distance =	Math.Pow((activeTemp.getMid(activeActor.Position) - passiveTemp.getMid(passiveActor.Position)).X, 2f) +
															Math.Pow((activeTemp.getMid(activeActor.Position) - passiveTemp.getMid(passiveActor.Position)).Y, 2f);

										if (distance < Math.Pow(activeTemp.SphereDiameter / 2f, 2f) + Math.Pow(passiveTemp.SphereDiameter / 2f, 2f))
										{

											Console.WriteLine(activeActor.ActorName + " " + passiveActor.ActorName);
											activeActor.IsOverlapping(passiveActor);

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

			//Max Velocity

			foreach (Actor actor in actors)
			{
				if (actor != null)
				{
					if (Math.Abs(actor.Velocity.X) + Math.Abs(actor.Velocity.Y) > actor.MaxVelocity && actor.MaxVelocity > 0)
					{

						int x = 1;
						int y = 1;

						if (actor.Velocity.X < 0)
						{
							x = -1;
						}
						if (actor.Velocity.Y < 0)
						{
							y = -1;
						}

						actor.Velocity = new Vector2f(actor.Velocity.X / (actor.Velocity.X + actor.Velocity.Y) * actor.MaxVelocity * x, actor.Velocity.Y / (actor.Velocity.X + actor.Velocity.Y) * actor.MaxVelocity * y);

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

		public bool RemoveActorFromGroup(Actor actor)
		{
			foreach (string listName in ActorGroups.Keys)
			{
				if (ActorGroups[listName].Contains(actor))
				{
					ActorGroups[listName].Remove(actor);
				}
			}
			return true;
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

				if (sphere.SphereDiameter * sphere.SphereDiameter > distanceX * distanceX + distanceY * distanceY)
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
					double distance = Math.Pow((activeTemp.Position.X + activeTemp.SphereDiameter ) - (passiveTemp.Position.X + passiveTemp.SphereDiameter) , 2f)+
									Math.Pow((activeTemp.Position.Y + activeTemp.SphereDiameter ) - (passiveTemp.Position.Y + passiveTemp.SphereDiameter), 2f);

					if (distance < (activeTemp.SphereDiameter*activeTemp.SphereDiameter) + (passiveTemp.SphereDiameter*passiveTemp.SphereDiameter))
					{
						return true;
					}
				}
			}
			return false;
		}

	    internal void Shutdown()
	    {
		    foreach (var list in ActorGroups)
		    {
			    list.Value.Clear();
		    }
		    ActorGroups.Clear();
		    foreach (var col in CollidablePartner)
		    {
			    col.Value.Clear();
		    }
		    CollidablePartner.Clear();
			foreach (var overlap in OverlapPartner)
			{
				overlap.Value.Clear();
			}
		    OverlapPartner.Clear();
	    }
	}
}