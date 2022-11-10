namespace Chase.FFmpeg.Events;

public class FFProcessUpdateEventArgs : EventArgs
{
    public float Percentage { get; set; }
    public uint FramesProcessed { get; set; }
    public float Speed { get; set; }
    public float AverageBitrate { get; set; }
}
