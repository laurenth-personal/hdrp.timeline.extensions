# High Definition RP timeline extensions

Timeline tracks for hdrp settings
- exposure
- DOF
- bloom
- vignette
- shadows

# Usage
- Add a track of the setting you want to animate in a timeline
- Reference a gameobject that has a volume (Global volume with higher priority than gameplay or other volumes)
- add a clip and tweak the settings
- you can blend clips to interpolate values

# WIP
The tracks are working but the way they override settings on the scene volume varies (some have explicit buttons "override ...", some have small checkboxes, for others you need to check the override on the volume profile yourself).
This is not ideal I'll work on an update if I can or contributors are welcome to unify this.
