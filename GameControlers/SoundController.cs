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

        public void Update(Megaman megaman, bool isPaused, List<Pellet> pellets)
        {
            // Check if the game is not paused
            if (!isPaused)
            {
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();
                }

                //Play shooting sound if MegaMan is shooting and the sound isn't already playing
                //if (megaman.is_shooting && !isShootingSoundPlaying)
                //{
                //    shootingSound.Play();
                //    isShootingSoundPlaying = true;  // Mark that shooting sound is playing
                //}
                //else if (!megaman.is_shooting && isShootingSoundPlaying)
                //{
                //    isShootingSoundPlaying = false;  // Stop playing sound when not shooting
                //}
                int currentPelletNum = pelletNum;
                if (pellets.Count != currentPelletNum)
                {
                    pelletNum = pellets.Count;
                    shootingSound.Play();
                }


                else
                {
                    if (MediaPlayer.State == MediaState.Playing)
                    {
                        MediaPlayer.Pause();
                    }
                }
            }
        }
    }
}