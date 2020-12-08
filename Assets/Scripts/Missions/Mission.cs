public class Mission {
  public MissionType type { get; private set; }
  public bool succeeded { get; private set; }

  public float time { get; private set; }

  public Mission(MissionType type, bool succeeded, float time) {
    this.type = type;
    this.succeeded = succeeded;
    this.time = time;
  }
}