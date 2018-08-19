# Planet2Space

Planet2Space by Juggernaut93 (with code from Gravity Aligner by p3st|cIdE)

**SETUP:**
   1) Create a group with a cockpit (or remote control or some other ship controller) to use as orientation reference and gyros.
      These blocks will be used by Gravity Aligner to align the ship in the correct direction
      (more info here: https://steamcommunity.com/sharedfiles/filedetails/?id=567481214).
      The group can also contain a Timer block to run after the ship reaches space.
   2) Run this script with argument: [GroupName];[true|false]
       - put true (default) if you want hydrogen thrusters to have priority over ion thrusters, false otherwise
       - default group name is "Aligner"
       - example: AlignerGroupName;true -> "AlignerGroupName" is the group name and you prefer using hydrogen thruster over ion ones
   3) Wait until the ship reaches space.
       - You can interrupt the script at any moment running it with argument "stop" (without quotes).
       - To change settings while ascending you MUST first stop the script and then rerun it with the different parameters.
       - MAKE SURE the ship can sustain its own weight!
       - Also, if you want your ship to stop after reaching space, be sure you have place thrusters of your preferred thruster
         type in each of the 6 orthogonal directions of the ship (especially the downward direction to stop the upward velocity).

**!!! WARNING !!!
IF YOU HAVE A SHIP WITH A COCKPIT/CONTROL STATION/ETC. WITH THE FRONT ORIENTED TOWARDS SPACE (I.E. ROCKET-LIKE) MAKE SURE TO
ADD A SECONDARY SHIP CONTROLLER (E.G. REMOTE CONTROL) ORIENTED IN THE "NORMAL WAY" (THE DOWNSIDE OF THE REMOTE CONTROLLER IS
ORIENTED TOWARDS THE PLANET = THE DIRECTION TOWARDS WHICH THE THRUSTER THAT SHOULD LIFT YOUR SHIP TO SPACE WILL PUSH).**

**HOW IT WORKS:**
   1) All landing gears (if present) are unlocked and all thrusters not pointing up are shut down
   2) Gravity Aligner is started to keep the ship oriented so that the selected cockpit has the "down" direction towards the planet
      (i.e. the natural orientation of the ship = NOT upside down :)). You can still manually rotate the ship around the gravity vector.
   3) When the ship is aligned (>99% of thrust will be applied downwards) and it has no significant lateral velocity (< 0.1m/s)
      the acceleration phase starts:
       - all atmo thrusters are set at full power
       - dampeners are disabled
       - if atmo thrusters can't provide at least 15 m/s^2 of acceleration upwards
         (~0.15g upwards, added to the acceleration required to overcome gravity),
         hydrogen and/or ion thrusters are added to the mix according to your specified priority
         (e.g. if you prefer hydrogen thrusters, ion will only be used if hydrogen thrusters aren't enough)
       - if for whatever reason the ship is falling at more than 2 m/s, all thrusters in the Up direction will be turned on at max
         when the downward velocity is higher than 2 m/s, the normal behavior is restored
   4) When the ship reaches 99.5 m/s against the gravity direction, thruster power is reduced to keep such velocity
      (speed could oscillate a bit before stabilizing)
       - unnecessary fuel consumption is eliminated!
       - priorities are still respected: first use atmos, then hydro/ions as specified
       - when atmos don't work anymore (thrust = 0N), they are shut down to avoid wasting electricity
         (full override with atmo is costly regardless of effective thrust)
   5) When 0 gravity is detected, the script will shut down (including the Gravity Aligner part), the dampeners are reactivated
      and all hydro/ion thrusters (according to preference) are enabled, allowing the ship to come to a full stop
   6) If a Timer is included in the [GroupName] group, it will start the countdown
