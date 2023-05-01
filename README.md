# Alien Planet Nature simulation

Name: Maciej Golubski , Luke Hallinan


Student Number: C19389881, D20125299

Class Group:DT228

# Description of the project
	 You are on an alien planet planet full creatures made up of basic shapes or simple designs.  Food is around the planet, each food is with a set of body segments or movements it might add to the creature fed. You will be able to pick up the creatures and put them in a fenced area to produce one that has a random selection of features from each parent like a loving happy family.
# Instructions for use
	The user can move in Virtual Reality using their two controllers
	The right controllers button is responsible for turning,
	where as the left controller is responsible for moving.
	the individual can walk on the various platforms to explore the world
    The invidividual is able to move the creature to the fence to make them reproduce

# How it works
### Model combined with camera
A model has been imported from the asset store of a robot.
this Model is then rigged to have both hand movements, leg movements and head movements.
##### Leg Movement 
To get the leg movement working,
We have followed a tutorial to get the legs to move in accordance to the rate of movement of the camera

# List of classes/assets in the project 

| Class/assets 				| Source |
|---------------------------|-----------|
| Arrive.cs 	            | Self written |e
| Boid.cs			        | Self written |e
| Seek.cs		            | Self written |
| Fence.cs	                | Self written |
| Fighter.cs			    | Self written |e
| Flee.cs	                | Self written |e
| FollowPath.cs             | Self written |e
| Food.cs	                | Self written |
| wonder.cs	                | Self written |
| LevelStart.cs	            | Self written |
| MoveObject.cs	            | Self written |
| wander.cs	                | Self written |
| Path.cs	                | Self written |e
| SpawnerScriot.cs	        | Self written |
| Head.cs					| Modified from [reference](https://www.youtube.com/watch?v=MYOjQICbd8I&list=PLwz27aQG0IIK88An7Gd16An9RrdCXKBAB&index=19) |
| Hand.cs					| Modified from [reference](https://www.youtube.com/watch?v=MYOjQICbd8I&list=PLwz27aQG0IIK88An7Gd16An9RrdCXKBAB&index=19)  |
| IKFootSolver.cs 			| From [reference](https://www.youtube.com/watch?v=1Xr3jB8ik1g&t=256s) |
| Robert Kyle				| Imported from asset store |
| fire						| Self written |
| skybox					| Self written |
| creature1					| Self written |
| creature2					| Self written |
| creature3				    | Self written |
| rib2					    | Self written |
| Cube						| Self written |
| Sphere					| Self written |
| Cylinder					| Self written |
| hip 1					    | Self written |
| hip 2					    | Self written |
| hip 3 				    | Self written |
| hip 4					    | Self written |
| hip 5					    | Self written |
| hip 6					    | Self written |
| ribs 					    | Self written |
| ribs 1					| Self written |
| ribs 2					| Self written |
| ribs 3				    | Self written |
| ribs 4					| Self written |
| ribs 5					| Self written |
| ribs 6					| Self written |
| terrain					| Self written |
| spaceship.fbx				| self created in blender |
| beetroot.fbx				| self created in blender |
| Cacti.fbx				    | self created in blender |
| carrot.fbx				| self created in blender |
| corn.fbx				    | self created in blender |
| onion.fbx				    | self created in blender |
| potato.fbx				| self created in blender |
| tomato.fbx				| self created in blender |
| wheat.fbx				    | self created in blender |
| fence.fbx				    | self created in blender |
| wings.fbx				    | self created in blender |
# References

# What I am most proud of in the assignment

# Proposal submitted earlier can go here:
	We will be making an alien planet nature simulation. You will be on an alien planet planet full creatures made up of basic shapes or simple designs.  Food will be around the planet, each with a set of body segments or movements it might add to the creature fed. You will be able to pick up the creatures and put them in a fenced area to produce one that has a random selection of features from each parent like a loving happy family.
## This is how to markdown text:

# class "scale from microphone"
```c#
void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if ( loudness<threshold)
            loudness =0;
        transform.localScale= Vector3.Lerp(minScale,maxScale,loudness);
    }
```
Gets the loudnessFromMicrophone class from a different file and compares it then it scales it relative to the variables
# class "GetLoudnessFromMicrophone"
```c#
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]),microphoneClip);
    }
```
Gets loudness from a clip 
# class "GetLoudnessFromAudioClip"
```c#
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition- sampleWindow;
        if(startPosition<0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData,startPosition);

        //compute loudness
        float totalLoudness = 0;
        for(int i = 0 ; i < sampleWindow; i++)
        {
            totalLoudness+= Mathf.Abs(waveData[i]);
        }
        return totalLoudness/ sampleWindow;
    }
```
Uses GetData to fetch data from the microphone which is in the form of a wave
# class "CircularMovement"
```c#
    void Update()
    {
        if (angle >= 360f)
            angle = 0f;
            posX =  centerRotation.position.x + Mathf.Cos (angle) * radiusRotation;
            posY =  centerRotation.position.y + Mathf.Sin (angle) * radiusRotation;
            posZ =  centerRotation.position.z + Mathf.Sin (angle) * radiusRotation;
            angle += Time.deltaTime * angularSpeed;
        

        transform.position = new Vector3(posX,posY,posZ);
    }
```
Makes the main spaceship rotate in a circular way
# class "ParticleAI"
```c#
    void Update()
    {
        otherParticleMesh.SetDestination(particleObj.position);
    }
```
Helps with Artificial intelligence of agents to follow the object
# class "Randomrotate"
```c#
    void Update()
    {
        rotateS = speed * Time.deltaTime;
        transform.Rotate(0, rotateS, 0);
    }
```
Rototate the hologram platforms

# Maciej Golubski
 Created assets, the space system,terrain, the creatures and functions related to it, Im really proud of the character swapping body parts, animations and different assets and spawning of creatures. I learned how to effectively swap body parts to a creature spawn
# Luke Hallinan 
 Created movement for creatures, food script and fence mechanism 
# Installed packages
Animation rigging 
physics 
probuilder
Artificial intelligence
XR interaction toolkit
Xr plugin management
terrain package
This is a youtube video of the project:

[![YouTube](http://img.youtube.com/vi/EQzX3p8FefY/0.jpg)](https://www.youtube.com/watch?v=EQzX3p8FefY)
