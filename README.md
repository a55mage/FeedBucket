# FeedBucket
A free Haptic Feedback library for VR applications in Unity

Made by [a55mage](https://github.com/a55mage) / [Daichip](https://github.com/daichip) / [Tizzu](https://github.com/tizzu)
# ❕ Features
- 4 ready to use vibration patterns
- Easy vibration pattern creation
- Automatic haptic feedback generation from audio file
- Works with Oculus, HTC Vive, Valve Index, Mixed Reality HMD... you name it

# ❔ How to setup
1) Install the Steam VR plugin for unity and set it up
2) Import the two scripts in your project
3) Add the FeedManager script to your Player object
4) In the inspector, go to the "Controller Vibration" field and select your Steam VR output profile

# ❓ How to use
(☞ﾟヮﾟ)☞ THE CODE HAVE PLENTY OF COMMENTS ☜(ﾟヮﾟ☜)

Read the instructions inside the scripts
To make your own haptic pattern you only need to know 3 things:
1) Go to FeedLibrary to create a list of Feed, insert feeds in it and create a corresponding boolean
2) Go to FeedManager and copy/paste the commented code in the right place and edit it
3) The library will execute the desired feedback when the corresponding boolean is set to true and reset it to false when the feedback ends

# 💬 Got questions?
Feel free to ask
