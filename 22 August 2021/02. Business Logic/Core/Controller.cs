using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astroRepo;
        private IRepository<IPlanet> planetRepo;
        private IMission mission;

        public Controller()
        {
            this.astroRepo = new AstronautRepository();
            this.planetRepo = new PlanetRepository();
            this.mission = new Mission();
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

            this.astroRepo.Add(astronaut);

            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            this.planetRepo.Add(planet);

            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            var astronauts = this.astroRepo.Models.Where(x => x.Oxygen >= 60).ToList();

            if (astronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            var planet = planetRepo.Models.FirstOrDefault(x => x.Name == planetName);

            this.mission.Explore(planet, astronauts);

            var deadAstronauts = astronauts.Where(x => x.Oxygen == 0).ToList().Count;

            foreach (var astr in astronauts.Where(x => x.Oxygen == 0))
            {
                this.astroRepo.Remove(astr);
            }

            return $"Planet: {planetName} was explored! Exploration finished with {deadAstronauts} dead astronauts!";
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.planetRepo.Models.Where(x => x.Items.Count == 0).ToList().Count} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var a in this.astroRepo.Models)
            {
                sb.AppendLine($"Name: {a.Name}");
                sb.AppendLine($"Oxygen: {a.Oxygen}");
                sb.Append($"Bag items: ");
                if (a.Bag.Items.Count == 0)
                {
                    sb.Append("none");
                }
                else
                {
                    for (int i = 0; i < a.Bag.Items.Count; i++)
                    {
                        if (i == a.Bag.Items.Count - 1)
                        {
                            sb.Append($"{a.Bag.Items.ToList()[i]}");
                        }
                        else
                        {
                            sb.Append($"{a.Bag.Items.ToList()[i]}, ");
                        }
                    }
                }

                if (this.astroRepo.Models.ToList().IndexOf(a) != this.astroRepo.Models.Count - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = this.astroRepo.Models.FirstOrDefault(x => x.Name == astronautName);

            if (astronaut == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            this.astroRepo.Remove(astronaut);

            return $"Astronaut {astronautName} was retired!";
        }
    }
}
