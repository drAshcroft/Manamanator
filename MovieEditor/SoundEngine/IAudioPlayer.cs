using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.SoundEngines
{
    /// <summary>
    /// Delegate used to fill in a buffer
    /// </summary>
    public delegate int PullAudioCallback(ref byte[] Buffer,int offset, int count);

    /// <summary>
    /// Audio player interface
    /// </summary>
    public interface IAudioPlayer : IDisposable
    {
        int SamplingRate { get; }
        int BitsPerSample { get; }
        int Channels { get; }

        int GetBufferedSize();
        void Play(PullAudioCallback onAudioData);
        void Stop();
    }
}
