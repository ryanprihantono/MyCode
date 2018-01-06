using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Windows.Forms;
//using Microsoft.DirectX.AudioVideoPlayback;

namespace SatoCarwashSystem.lib
{
    class Sound
    {
        private SoundPlayer player;
        private List<String> sequence;
        private String audioDirectory;
        private int index = 0;

        private Timer timer;
        //private List<Audio> audio;

        public Sound()
        {
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            sequence = new List<string>();
            audioDirectory = Directory.GetCurrentDirectory() + "\\audio\\";
            player = new SoundPlayer();
        }
        //public Sound()
        //{
        //    audio = new List<Audio>();
        //    audioDirectory = Directory.GetCurrentDirectory() + "\\audio\\";
            
        //}
        //public void play(String nopol)
        //{
        //    audio.Add((new Audio(audioDirectory +"ding.wav")));
        //    audio.Add((new Audio(audioDirectory + "awal.wav")));
        //    for (int i = 0; i < nopol.Length; i++)
        //    {
        //        audio.Add(new Audio(audioDirectory + nopol.Substring(i, 1) + ".wav"));
                
        //    }
        //    audio.Add((new Audio(audioDirectory + "akhir.wav")));
        //    audio[index].Play();
        //    audio[index].Ending += new EventHandler(Sound_Ending);
        //}

        //void Sound_Ending(object sender, EventArgs e)
        //{
        //    audio[index].Stop();
        //    audio[index].Ending -= new EventHandler(Sound_Ending);
        //    index++;
        //    if (index < audio.Count)
        //    {
        //        audio[index].Play();
        //        audio[index].Ending += new EventHandler(Sound_Ending);
        //    }
        //}

        void timer_Tick(object sender, EventArgs e)
        {
            playSequence();
        }

        public void playNopol(String nopol)
        {
            sequence.Add("ding");
            sequence.Add("awal");
            for (int i = 0; i < nopol.Length; i++)
            {
                sequence.Add(nopol.Substring(i, 1));
            }
            sequence.Add("akhir");
            playSequence();

        }
        private void playSequence()
        {
            if (index < sequence.Count)
            {
                if (index > 0 && sequence[index].Length > 1)
                {
                    timer.Interval = 3000;
                }
                else if (index == 0)
                {
                    timer.Interval = 2000;
                }
                else
                {
                    timer.Interval = 1000;
                }
                player.SoundLocation = audioDirectory + sequence[index] + ".wav";
                player.Load();
                //player.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(player_LoadCompleted);
                player.Play();
                if (index == 0)
                {
                    timer.Start();
                }
                index++;
                if (index >= sequence.Count)
                {
                    timer.Stop();
                }
            }
        }

        
        private void playSound(String soundName)
        {
            player.SoundLocation = audioDirectory + soundName + ".mp3";
            player.Load();
            player.Play();
        }
    }
}
