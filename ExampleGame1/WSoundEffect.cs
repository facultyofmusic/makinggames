using SharpDX.IO;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace ExampleGame1
{
    class WSoundEffect
    {
        readonly XAudio2 _xaudio;
        readonly WaveFormat _waveFormat;
        readonly AudioBuffer _buffer;
        readonly SoundStream _soundstream;

        public WSoundEffect(string soundFxPath)
        {
            _xaudio = new XAudio2();
            var masteringsound = new MasteringVoice(_xaudio);

            var nativefilestream = new NativeFileStream(
            soundFxPath,
            NativeFileMode.Open,
            NativeFileAccess.Read,
            NativeFileShare.Read);

            _soundstream = new SoundStream(nativefilestream);
            _waveFormat = _soundstream.Format;
            _buffer = new AudioBuffer{
                Stream = _soundstream.ToDataStream(),
                AudioBytes = (int) _soundstream.Length,
                Flags = BufferFlags.EndOfStream
                };
        }

        public void Play()
        {
            var sourceVoice = new SourceVoice(_xaudio, _waveFormat, true);
            sourceVoice.SubmitSourceBuffer(_buffer, _soundstream.DecodedPacketsInfo);
            sourceVoice.Start();
        }
    }
}
