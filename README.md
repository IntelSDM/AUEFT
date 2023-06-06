# AUEFT
An EFT trainer from many many years ago. Just another old project that I am publishing for the sake of it. The bypass was detected long ago, all the auth stuff was stripped, and it is now the injection logic and the internal components. Auth module is also removed so some stuff might not make sense.
<br>

Keep in mind that this is now redundant, old and completely outdated.
<br>
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/ss3.png" width=70% height=80%>
<br>

# What is AUEFT?
AUEFT is a trainer for the game that modifies its memory through the use of the mono library it was made many years ago as a sold product that had quite a few users at its peak.

<br>
This was made over a week or two, I didn't want to upset the original repo by unarchiving it to add comments and remove sensitive data about the authentication system. This wasn't up to any great standard but it worked and worked rather well.
<br>
Even though it was made rather quickly it was maintained for about 5 months while it was up for sale and eventually got detected a few times in that span for obvious reasons. It did what it was meant to do though.
A lot of the processing might seem weird and that's because it uses multiple threads which are technically invisible to the machine at an OS level due to managed memory with .NET which is why i used a thread in the first place.
The code is very regretable and ugly since its old, it was rushed so dont expect to use anything in it. It did have multi lingual support but that wasn't any big leap in technology, just another parameter.
<br>

# Bypassing Battleye
Obviously this was detected long ago but at its time it handled battleye rather well by manipulating a flaw in their integrity check. 
<br>
Lets see the integrity check shellcode recreation published by secret club from 3 years ago which the bypass was based around:
<br>
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/shellcode.png" width=50% height=80%>
<br>

It loops through all of the files from these set paths and checks the image size. 
<br>
So all we had to do was change the file path names, in unity you can do this by changing the exe name and then the data path name.
This can be done through the belauncher.ini file which holds the game directory. This resulted in the ability to fully alter the folder structure defeating the integrity checks.
Obviously the overlay can easily be detected and probably is. This way of defeating the integrity checks has also been long patched.
In a product standpoint I replicated the bypass on each user's machine simply by copying the game's path to another folder, changing the folder heirarchy and loading the modified folder structure.
<br>

# Comments On The System
The code was rushed and awful but in practice the product worked well, the idea for the bypass was rather good espeically for coming out right after they created this new integrity check system 3 years ago.
A lot of the code was never meant to see the light of day. lack of object oriented programming, lack of obfuscation implementation, lack of organization, over using embedded if statements. All the typical stuff in a messy prototype project that was taken way too far past the prototype phase. 

<br>
# Images:
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/ss1.png" width=70% height=80%>
<br>
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/ss2.png" width=70% height=80%>
<br>
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/ss3.png" width=70% height=80%>
<br>
<img src="https://github.com/IntelSDM/AUEFT/blob/master/Images/ss4.png" width=70% height=80%>
<br>

# Videos:
https://github.com/IntelSDM/AUEFT/blob/master/Videos/Video1.mp4

https://github.com/IntelSDM/AUEFT/blob/master/Videos/Video2.mp4
