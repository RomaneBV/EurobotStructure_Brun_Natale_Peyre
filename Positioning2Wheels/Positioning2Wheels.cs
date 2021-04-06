using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        int robotId;
        Location robotLocation = new Location(); //1, 1, Math.PI / 3, 0, 0, 0);
        public Positioning2Wheels(int id)
        {
            robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            /// Ajoutez votre code de calcul de la nouvelle position ici
            int dt = 1 / 50;
            robotLocation.X += e.Vx * dt;
            robotLocation.Y += e.Vy * dt;
            robotLocation.Theta += e.Vtheta * dt;

            /// Ajoutez l'appel à l'event de transmission de la position calculée ici
            OnCalculatedLocation(robotId, robotLocation);
        }

        //Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
