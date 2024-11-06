using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Utility
{
	/// <summary>
	/// Generic Actor Spawner. Searches all active assemblies for any subtype of Actor that are not abstract and a class and creates all possible constructor lambdas from them.
	/// <para/>This allows efficient generic Actor spawning without caring about the specifics of adding an actor to a level, at very little performance cost.
	/// <para/>See <see href="http://mattgabriel.co.uk/2016/02/10/object-creation-using-lambda-expression/">Matt Gabriel's Blog Entry</see> for details.
	/// <para/>Tip: This class could be made generic with a non-generic ActorSpawner wrapper class. This requires the actual spawning to be exported to the wrapper class.
	/// </summary>
	public class ActorSpawner
	{
		private List<Type> ObjectTypes { get; } = [];
		private Dictionary<Type, Creator<Actor_OLD>> ObjectConstructors { get; } = new();

		private delegate T Creator<out T>(params object[] args);

		public ActorSpawner()
		{
			ObjectTypes.Add(typeof(Actor_OLD));
			FindObjectSubTypes();
			SpawnTypeCreator();
		}

		/// <summary>
		/// Finds all of the Objects Subtypes
		/// </summary>
		public void FindObjectSubTypes()
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in assembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Actor_OLD))))
				{
					ObjectTypes.Add(type);
				}
			}
		}

		private void SpawnTypeCreator()
		{
			foreach (var objectType in ObjectTypes)
			{
				ConstructorInfo[] constructors = objectType.GetConstructors();
				if (constructors.Length >= 0) // Should be > 0?
				{
					ConstructorInfo constructor = constructors[0];

					ParameterInfo[] paramsInfo = constructor.GetParameters();

					if (paramsInfo.Length > 0)
					{
						ParameterExpression param = Expression.Parameter(typeof(object[]), "args");
						Expression[] argsExpressions = new Expression[paramsInfo.Length];
						for (var i = 0; i < paramsInfo.Length; ++i)
						{
							Expression index = Expression.Constant(i);
							Type paramType = paramsInfo[i].ParameterType;
							Expression paramAccessorExp = Expression.ArrayIndex(param, index);
							Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
							argsExpressions[i] = paramCastExp;
						}

						NewExpression newExpression = Expression.New(constructor, argsExpressions);

						LambdaExpression lambda = Expression.Lambda(typeof(Creator<Actor_OLD>), newExpression, param);

						Creator<Actor_OLD> compiled = (Creator<Actor_OLD>)lambda.Compile();
						ObjectConstructors.Add(objectType, compiled);
					}
				}
			}
		}

		public T SpawnObject<T>(params object[] args) where T : Actor_OLD
		{
			Creator<Actor_OLD> createdActivator = ObjectConstructors[typeof(T)];
			return createdActivator(args) as T;
		}

		public object SpawnObject(Type actorType, params object[] args)
		{
			Creator<Actor_OLD> createdActivator = ObjectConstructors[actorType];
			return createdActivator(args);
		}
	}
}