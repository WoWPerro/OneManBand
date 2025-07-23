public class Note
{
    public int type;
    public float time;
    public float timeToTap;

    public Note(int _type, float _time, float _timeToTap)
    {
        type = _type;
        time = _time;
        timeToTap = _timeToTap;
    }

    public int GetNoteType() { return type; }
    public float GetTime() { return time; }
    public float GetTimeToTap() { return timeToTap; }
}