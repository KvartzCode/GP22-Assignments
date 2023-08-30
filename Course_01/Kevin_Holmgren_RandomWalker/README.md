# Random Walker

Part of a school assignment to practice the use of interfaces. The idea of this project is that my teacher will run my [script](Assets/RandomWalkers/KevHol.cs) along with my other classmates' scripts in an game environment where our classes will fight to cover the most surface on a play area.

I was only given an interface with no information on how it will be handled in the teachers environment.

## Note
Since this assignment's only requirements were only that I have to implement an interface in a script. I was allowed to use other methods to affect the game. Note that the [script](Assets/RandomWalkers/KevHol.cs) will do several things to keep in mind.

It will:
- Use user32.dll to directly change your computers System Sounds volume (it will set the volume to 16)
- Make web calls to recieve an mp3 file.
- Convert a Base64 string to byte array and apply it to a sprite.