namespace SimulationSystem
{
    public class SimulationLifecyleMethods
    {
        public EasyECSController ecsController;

        public SimulationLifecyleMethods(EasyECSController ecsController)
        {
            this.ecsController = ecsController;
        }
        
        public void Awake()
        {
            ecsController.Awake();
        }

        public void Start()
        {
            ecsController.Start();
        }

        public void Update()
        {
            ecsController.Update();
        }

        //TODO: fixedupdate()
        
        public void LateUpdate()
        {
            ecsController.LateUpdate();
        }

        public void Render()
        {
            ecsController.Render();
        }

        //TODO: on destroy
        
        public void OnSimulationQuit()
        {
            ecsController.OnApplicationQuit();
        }
    }
}