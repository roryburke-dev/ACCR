using System;

namespace ACCR.Utils
{
    public interface IState
    {
        public ACCR.ICoordinator? Coordinator { get; set; }
        public event EnterEventHandler OnEnter;
        public event ExecuteEventHandler OnExecute;
        public event ExitEventHandler OnExit;
        public void Enter();
        public void Execute();
        public void Exit();
    }
    public delegate void EnterEventHandler(object sender, EventArgs e);
    public delegate void ExecuteEventHandler(object sender, EventArgs e);
    public delegate void ExitEventHandler(object sender, EventArgs e);
}