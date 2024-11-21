using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Project1.GameObjects;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Project1.GameControllers
{
    public class soundController
    {
        private ContentManager _content;
        private Song song; // Background music
        private SoundEffect shootingSound; // Shooting sound effect
        private bool isShootingSoundPlaying;
        private int pelletNum = 0;
        private bool paused = false;

        public soundController(ContentManager content)
        {
            _content = content;
            isShootingSoundPlaying = false;
        }

        public void Initialize()
        {
            // Load background music and play it in a loop
            song = _content.Load<Song>("06 Bombman Stage");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            //shootingSound = _content.Load<SoundEffect>("shooting-101soundboards");
        }

        public void LoadContent()
        {
            //Load the shooting sound effect here
            shootingSound = _content.Load<SoundEffect>("tx0_fire1");

        }

        public void Update(Megaman megaman, bool isPaused, List<Pellet> pellets, bool megamanDead)
        {
            // Check if the game is not paused
            if (!isPaused && !megamanDead)
            {
                if (paused)
                {
                    MediaPlayer.Resume();
                    paused = false;
                }
                
                int currentPelletNum = pelletNum;
                if (pellets.Count != currentPelletNum)
                {
                    pelletNum = pellets.Count;
                    shootingSound.Play();
                }


                
            }
            else
            {
                paused = true;
                MediaPlayer.Pause();
            }
        }
    }
}