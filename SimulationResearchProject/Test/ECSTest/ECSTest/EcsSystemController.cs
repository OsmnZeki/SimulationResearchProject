using Dalak.Ecs;

namespace ECSTest
{
    public abstract class EcsSystemController
    {
        World world;
        SystemManager systemManager;

        public void Awake()
        {
            world = new World();
            systemManager = new SystemManager(world);

            AddSystems();

            OnInject();
            systemManager.Awake();
        }

        public void Start()
        {
            systemManager.Start();
        }

        public void FixedUpdate()
        {
            //DTime.fixedDeltaTime = Time.fixedDeltaTime;
            systemManager.FixedUpdate();
        }

        public void Update()
        {
            //DTime.deltaTime = Time.deltaTime;
            systemManager.Update();
        }

        public void LateUpdate()
        {
            systemManager.LateUpdate();
        }


        public abstract void OnInject();
        public abstract void AddSystems();
    }
}