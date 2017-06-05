using System.Collections.Generic;
using SFML.System;
using SFML_Engine.Engine.Game;

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
		/*
		internal void PhysicsTick(float deltaTime, ref List<Actor> actors)
        {
			// move Actors

			foreach (Actor actor in actors)
			{
				if (actor != null)
				{
					if (actor.Movable)
					{
						actor.MoveAbsolute(actor.Position += actor.Velocity * deltaTime);

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
									double distanceX = 0;// Math.Min(Math.Abs(boxActor.Position.X - sphere.GetMid(sphereActor.Position).X), Math.Abs(boxActor.Position.X + box.BoxExtent.X - sphere.GetMid(sphereActor.Position).X));
									double distanceY = 0;// Math.Min(Math.Abs(boxActor.Position.Y - sphere.GetMid(sphereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.BoxExtent.Y - sphere.GetMid(sphereActor.Position).Y));

									if (Math.Abs(sphere.GetMid(sphereActor.Position).X - boxActor.Position.X) < Math.Abs(sphere.GetMid(sphereActor.Position).X - (boxActor.Position.X + box.CollisionBounds.X)))
									{
										distanceX = sphere.GetMid(sphereActor.Position).X - boxActor.Position.X;
									}
									else
									{
										distanceX = sphere.GetMid(sphereActor.Position).X - (boxActor.Position.X + box.CollisionBounds.X);
									}

									if (Math.Abs(sphere.GetMid(sphereActor.Position).Y - boxActor.Position.Y) < Math.Abs(sphere.GetMid(sphereActor.Position).Y - (boxActor.Position.Y + box.CollisionBounds.Y)))
									{
										distanceY = sphere.GetMid(sphereActor.Position).Y - boxActor.Position.Y;
									}
									else
									{
										distanceY = sphere.GetMid(sphereActor.Position).Y - (boxActor.Position.Y + box.CollisionBounds.Y);
									}

									//isInside
									if (boxActor.Position.X < sphere.GetMid(sphereActor.Position).X && boxActor.Position.X + box.CollisionBounds.X > sphere.GetMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.GetMid(sphereActor.Position).Y && boxActor.Position.Y + box.CollisionBounds.Y > sphere.GetMid(sphereActor.Position).Y)
									{ 

										activeActor.IsOverlapping(passiveActor);

										if (boxActor.Position.Y + box.CollisionBounds.Y/2f > sphere.GetMid(sphereActor.Position).Y)
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);
												
												Vector2f norm = sphere.GetMid(sphereActor.Position) - box.GetMid(boxActor.Position);

												norm = new Vector2f(norm.X / (Math.Abs(norm.X) + Math.Abs(norm.Y)), norm.Y / (Math.Abs(norm.X) + Math.Abs(norm.Y)));

												sphereActor.Velocity = new Vector2f((Math.Abs(sphereActor.Velocity.X) + Math.Abs(sphereActor.Velocity.Y)) * norm.X, (Math.Abs(sphereActor.Velocity.X) + Math.Abs(sphereActor.Velocity.Y)) * norm.Y);
												sphereActor.Acceleration = new Vector2f((Math.Abs(sphereActor.Acceleration.X) + Math.Abs(sphereActor.Acceleration.Y)) * norm.X, (Math.Abs(sphereActor.Acceleration.X) + Math.Abs(sphereActor.Acceleration.Y)) * norm.Y);


												activeActor.AfterCollision(passiveActor);
												
											}
										}
									}//box comes from right 
									else if (boxActor.Position.X < sphereActor.Position.X + sphere.CollisionBounds.X && boxActor.Position.X > sphereActor.Position.X &&
										boxActor.Position.Y < sphere.GetMid(sphereActor.Position).Y && boxActor.Position.Y + box.CollisionBounds.Y > sphere.GetMid(sphereActor.Position).Y)
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
									else if (boxActor.Position.X + box.CollisionBounds.X > sphereActor.Position.X && boxActor.Position.X + box.CollisionBounds.X < sphere.GetMid(sphereActor.Position).X &&
										boxActor.Position.Y < sphere.GetMid(sphereActor.Position).Y && boxActor.Position.Y + box.CollisionBounds.Y > sphere.GetMid(sphereActor.Position).Y)
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
									else if (boxActor.Position.Y < sphereActor.Position.Y + sphere.CollisionBounds.X && boxActor.Position.Y > sphereActor.Position.Y &&
										boxActor.Position.X < sphere.GetMid(sphereActor.Position).X && boxActor.Position.X + box.CollisionBounds.X > sphere.GetMid(sphereActor.Position).X)
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
									else if (boxActor.Position.Y + box.CollisionBounds.Y > sphereActor.Position.Y && boxActor.Position.Y + box.CollisionBounds.Y < sphere.GetMid(sphereActor.Position).Y &&
										boxActor.Position.X < sphere.GetMid(sphereActor.Position).X && boxActor.Position.X + box.CollisionBounds.X > sphere.GetMid(sphereActor.Position).X)
									{
										activeActor.IsOverlapping(passiveActor);

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{
												activeActor.BeforeCollision(passiveActor);

												Console.WriteLine(groupNameActive+" "+groupNamePassive);

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
									}//bounce from eage
									else if ((sphere.CollisionBounds.X / 2) * (sphere.CollisionBounds.X / 2) > distanceX * distanceX + distanceY * distanceY)
									{

										if (CollidablePartner.ContainsKey(groupNameActive))
										{
											if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
											{

												activeActor.IsOverlapping(passiveActor);

												double normTempX = distanceX / (Math.Abs(distanceX) + Math.Abs(distanceY));
												double normTempY = distanceY / (Math.Abs(distanceX) + Math.Abs(distanceY));

												//dumb Temp

												activeActor.BeforeCollision(passiveActor);

												float sphereSpeed = Math.Abs(sphereActor.Velocity.X) + Math.Abs(sphereActor.Velocity.Y);// + Math.Abs(boxActor.Velocity.X) + Math.Abs(boxActor.Velocity.Y);

												sphereActor.Velocity = new Vector2f((float)normTempX * sphereSpeed, (float)normTempY * sphereSpeed);

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

										if (activeActor.Position.X < passiveActor.Position.X + passiveTemp.CollisionBounds.X &&
											activeActor.Position.X + activeTemp.CollisionBounds.X > passiveActor.Position.X &&
											activeActor.Position.Y < passiveActor.Position.Y + passiveTemp.CollisionBounds.Y &&
											activeTemp.CollisionBounds.Y + activeActor.Position.Y > passiveActor.Position.Y
											)
										{
											activeActor.IsOverlapping(passiveActor);

											if (CollidablePartner.ContainsKey(groupNameActive))
											{
												if (CollidablePartner[groupNameActive].Contains(groupNamePassive))
												{
													activeActor.BeforeCollision(passiveActor);
													//X
													if ((Math.Abs(activeTemp.GetMid(activeActor.Position).X - passiveTemp.GetMid(passiveActor.Position).X)) < (activeTemp.CollisionBounds.X + passiveTemp.CollisionBounds.X)/2f &&
															(Math.Abs(activeTemp.GetMid(activeActor.Position).X - passiveTemp.GetMid(passiveActor.Position).X)) > Math.Max(activeTemp.CollisionBounds.X/2f,  passiveTemp.CollisionBounds.X/2f))
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
													if ((Math.Abs(activeTemp.GetMid(activeActor.Position).Y - passiveTemp.GetMid(passiveActor.Position).Y)) < (activeTemp.CollisionBounds.Y + passiveTemp.CollisionBounds.Y)/2f &&
														(Math.Abs(activeTemp.GetMid(activeActor.Position).Y - passiveTemp.GetMid(passiveActor.Position).Y)) > Math.Max(activeTemp.CollisionBounds.Y/2f, passiveTemp.CollisionBounds.Y/2f))
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

										double distance =	Math.Pow((activeTemp.GetMid(activeActor.Position) - passiveTemp.GetMid(passiveActor.Position)).X, 2f) +
															Math.Pow((activeTemp.GetMid(activeActor.Position) - passiveTemp.GetMid(passiveActor.Position)).Y, 2f);

										if (distance < Math.Pow(activeTemp.CollisionBounds.X / 2f, 2f) + Math.Pow(passiveTemp.CollisionBounds.X / 2f, 2f))
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

						Vector2f norm = new Vector2f(actor.Velocity.X / (Math.Abs(actor.Velocity.X) + (Math.Abs(actor.Velocity.Y))), actor.Velocity.Y / (Math.Abs(actor.Velocity.X) + (Math.Abs(actor.Velocity.Y))));

						actor.Velocity = new Vector2f(norm.X * actor.MaxVelocity, norm.Y * actor.MaxVelocity);
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

		public bool RemoveActorFromGroups(Actor actor)
		{
			bool removal = false;
			foreach (var listName in ActorGroups.Keys)
			{
				if (ActorGroups[listName].Contains(actor))
				{
					removal = ActorGroups[listName].Remove(actor);
				}
			}
			return removal;
		}

		public int RemoveActorFromGroups(uint actorID)
		{
			int removal = 0;
			foreach (var listName in ActorGroups.Keys)
			{
				removal = ActorGroups[listName].RemoveAll(actor => actor.ActorID == actorID);
			}
			return removal;
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
			if (CollidablePartner.ContainsKey(activ))
			{
				if (!CollidablePartner[activ].Contains(passive))
				{
					return true;
				}

				Console.Write(activ );

				foreach (String s in CollidablePartner[activ])
				{
					Console.Write(s + " ");
				}

				CollidablePartner[activ].Remove(passive);

				foreach (String s in CollidablePartner[activ])
				{
					Console.Write(s+" ");
				}

				return true;
			}
			return true;
		}

		/*
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

				double distanceX = Math.Min(Math.Abs(boxActor.Position.X - sphere.GetMid(sphereActor.Position).X), Math.Abs(boxActor.Position.X + box.CollisionBounds.X - sphere.GetMid(sphereActor.Position).X));
				double distanceY = Math.Min(Math.Abs(boxActor.Position.Y - sphere.GetMid(sphereActor.Position).Y), Math.Abs(boxActor.Position.Y + box.CollisionBounds.X - sphere.GetMid(sphereActor.Position).Y));

				if (sphere.CollisionBounds.X * sphere.CollisionBounds.X > distanceX * distanceX + distanceY * distanceY)
				{
					return true;
				}
				else if ((boxActor.Position.X < sphere.GetMid(sphereActor.Position).X && boxActor.Position.Y > sphere.GetMid(sphereActor.Position).Y && boxActor.Position.Y + box.CollisionBounds.Y < sphere.GetMid(sphereActor.Position).Y) ||
						(boxActor.Position.X + box.CollisionBounds.X > sphere.GetMid(sphereActor.Position).X && boxActor.Position.Y > sphere.GetMid(sphereActor.Position).Y && boxActor.Position.Y + box.CollisionBounds.Y < sphere.GetMid(sphereActor.Position).Y) ||
													(boxActor.Position.Y < sphere.GetMid(sphereActor.Position).Y && boxActor.Position.X > sphere.GetMid(sphereActor.Position).X && boxActor.Position.X + box.CollisionBounds.X < sphere.GetMid(sphereActor.Position).X) ||
						(boxActor.Position.Y + box.CollisionBounds.Y > sphere.GetMid(sphereActor.Position).Y && boxActor.Position.X > sphere.GetMid(sphereActor.Position).X && boxActor.Position.X + box.CollisionBounds.X < sphere.GetMid(sphereActor.Position).X)
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

					if (activeTemp.Position.X < passiveTemp.Position.X + passiveTemp.CollisionBounds.X &&
						activeTemp.Position.X + activeTemp.CollisionBounds.X > passiveTemp.Position.X &&
						activeTemp.Position.Y < passiveTemp.Position.Y + passiveTemp.CollisionBounds.Y &&
						activeTemp.Position.Y + activeTemp.CollisionBounds.Y > passiveTemp.Position.Y
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
					double distance = Math.Pow((activeTemp.Position.X + activeTemp.CollisionBounds.X) - (passiveTemp.Position.X + passiveTemp.CollisionBounds.X) , 2f)+
									Math.Pow((activeTemp.Position.Y + activeTemp.CollisionBounds.X) - (passiveTemp.Position.Y + passiveTemp.CollisionBounds.X), 2f);

					if (distance < (activeTemp.CollisionBounds.X * activeTemp.CollisionBounds.X) + (passiveTemp.CollisionBounds.X * passiveTemp.CollisionBounds.X))
					{
						return true;
					}
				}
			}
			return false;
		}*/
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