namespace Utils
{
    public interface IStateMachine
    {
        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }
        public IState GetCurrentState();
        public IState GetPreviousState();
        public void ChangeState(IState newState);
        public void UpdateState();
    }
}