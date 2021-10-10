using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Utilities.Messages;
using System;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astroRepo;
        private IRepository<IPlanet> planetRepo;

        public Controller()
        {
            this.astroRepo = new AstronautRepository();
            this.planetRepo = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;

            switch (type)
            {
                case "Biologist": astronaut = new Biologist(astronautName);
                    break;
                case "Geodesist": astronaut = new Geodesist(astronautName);
                    break;
                case "Meteorologist": astronaut = new Meteorologist(astronautName);
                    break;
                default: 
                    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            throw new NotImplementedException();
        }

        public string ExplorePlanet(string planetName)
        {
            throw new NotImplementedException();
        }

        public string Report()
        {
            throw new NotImplementedException();
        }

        public string RetireAstronaut(string astronautName)
        {
            throw new NotImplementedException();
        }
    }
}
