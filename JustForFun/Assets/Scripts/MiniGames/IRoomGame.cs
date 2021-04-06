public interface IRoomGame
{
    void SetState(GamePoolSrc.GameState state);
    GamePoolSrc.GameState State { get; set; }
    float ProgressValue { get; }
}
