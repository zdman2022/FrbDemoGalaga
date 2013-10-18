using FrbDemoGalaga.Entities.Bullets;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using FrbDemoGalaga.Performance;

namespace FrbDemoGalaga.Factories
{
	public class PlayerBulletFactory : IEntityFactory
	{
		public static PlayerBullet CreateNew ()
		{
			return CreateNew(null);
		}
		public static PlayerBullet CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			PlayerBullet instance = null;
			instance = mPool.GetNextAvailable();
			if (instance == null)
			{
				mPool.AddToPool(new PlayerBullet(mContentManagerName, false));
				instance =  mPool.GetNextAvailable();
			}
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (mBaseScreenListReference != null)
			{
				mBaseScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<PlayerBullet> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
			FactoryInitialize();
		}
		
		public static void Initialize (PositionedObjectList<Bullet> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mBaseScreenListReference = listFromScreen;
			FactoryInitialize();
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				PlayerBullet instance = new PlayerBullet(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (PlayerBullet objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (PlayerBullet objectToMakeUnused, bool callDestroy)
		{
			mPool.MakeUnused(objectToMakeUnused);
			
			if (callDestroy)
			{
				objectToMakeUnused.Destroy();
			}
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<PlayerBullet> mScreenListReference;
			static PositionedObjectList<Bullet> mBaseScreenListReference;
			static PoolList<PlayerBullet> mPool = new PoolList<PlayerBullet>();
			public static Action<PlayerBullet> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return PlayerBulletFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return PlayerBulletFactory.CreateNew(layer);
			}
			public static PositionedObjectList<PlayerBullet> ScreenListReference
			{
				get
				{
					return mScreenListReference;
				}
				set
				{
					mScreenListReference = value;
				}
			}
			static PlayerBulletFactory mSelf;
			public static PlayerBulletFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new PlayerBulletFactory();
					}
					return mSelf;
				}
			}
	}
}
