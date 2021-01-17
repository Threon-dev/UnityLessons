public interface IGameManager 
{ 
    ManagerStatus status { get; }
    void Startup(NetworkSerivce serivce);
}