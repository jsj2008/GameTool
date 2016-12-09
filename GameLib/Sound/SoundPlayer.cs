using System;
using System.IO;

namespace PublicUtilities
{
    public static class SoundPlayer
    {
        const string WarnFile = "sound\\warn.wav";
        const string AlterFile = "sound\\alter.wav";
        static System.Media.SoundPlayer warnPlayer = null;
        static SoundPlayer()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string soundPath = Path.Combine(appPath, WarnFile);
            if (File.Exists(soundPath))
            {
                warnPlayer = new System.Media.SoundPlayer(soundPath);
            }
        }

        public static bool IsMute { get; set; }

        public static void PlayWarn()
        {
            if (!IsMute && (null != warnPlayer))
            {
                warnPlayer.Play();
                //warnPlayer.PlayLooping();
            }
        }

        public static void StopWarn()
        {
            // IsMute = !IsMute;
            if (null != warnPlayer)
            {
                warnPlayer.Stop();
            }
        }

        public static void PlayAlter()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string soundPath = Path.Combine(appPath, AlterFile);
            if (File.Exists(soundPath))
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundPath);
                player.Play();
            }
        }
    }
}
