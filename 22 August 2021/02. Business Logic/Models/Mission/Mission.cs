using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            var readyAstronauts = astronauts.Where(x => x.Oxygen > 0).ToList();

            IAstronaut currentAstronaut;

            while (planet.Items.Count > 0 && readyAstronauts.Where(x=> x.Oxygen > 0).ToList().Count > 0)
            {
                currentAstronaut = readyAstronauts.FirstOrDefault(x => x.Oxygen > 0);

                if (currentAstronaut == null)
                {
                    break;
                }

                currentAstronaut.Bag.Items.Add(planet.Items.FirstOrDefault());
                currentAstronaut.Breath();
                planet.Items.Remove(planet.Items.FirstOrDefault());
            }
        }
    }
}
