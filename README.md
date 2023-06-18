# High Definition RP timeline extensions

Timeline tracks for hdrp settings
- exposure
- DOF
- bloom
- vignette
- shadows
- Probe volumes

# Usage
- Add a track of the setting you want to animate in a timeline
- Reference a gameobject that has a volume (Global volume with higher priority than gameplay or other volumes)
- add a clip and tweak the settings
- you can blend clips to interpolate values
## Probe volumes
- Create a track
- Create clips
- Enter the name of the lighting scenarios you want to blend
- Blend the clips to blend the lighting scenario probes
- 
# WIP
The tracks are working but the way they override settings on the scene volume varies (some have explicit buttons "override ...", some have small checkboxes, for others you need to check the override on the volume profile yourself).
This is not ideal I'll work on an update if I can or contributors are welcome to unify this.

# How to install (2020.2 and Newer) (thanks peeweek I copied your text :D )
- In Unity, Open Project Settings Window (Edit/Project Settings) and navigate to Package Manager
- Add a new Scoped Registry that references the openupm registry: https://package.openupm.com
- Add the following scopes to the OpenUPM Scoped Registry : com.laurenth
- Open the Package Manager window (Window/Package Manager) and Select Packages : My Registries in the toolbar
- Select this package in the list, then click the Install Button
