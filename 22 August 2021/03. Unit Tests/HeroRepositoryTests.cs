using System;
using System.Collections.Generic;
using NUnit.Framework;

public class HeroRepositoryTests
{
    [Test]
    public void CreateAValidHeroThatDoesntExist()
    {
        var heroRepository = new HeroRepository();
            
        var hero = new Hero("bobo", 55);

        heroRepository.Create(hero);

        Assert.IsTrue(heroRepository.Heroes.Count == 1);
    }

    [Test]
    public void CreateAValidHeroThatExists()
    {
        var heroRepository = new HeroRepository();

        var hero = new Hero("bobo", 55);

        heroRepository.Create(hero);

        try
        {
            heroRepository.Create(hero);
        }
        catch (InvalidOperationException ex)
        {
            Assert.IsTrue(heroRepository.Heroes.Count == 1 && ex.Message == $"Hero with name {hero.Name} already exists");
        }
    }

    [Test]
    public void CreateAnInvalidHero()
    {
        var heroRepository = new HeroRepository();

        try
        {
            heroRepository.Create(null);
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(heroRepository.Heroes.Count == 0 && ex.Message == "Hero is null (Parameter 'hero')");
        }
    }

    [Test]
    public void RemoveAnInvalidHeroWithNullArgument()
    {
        var heroRepository = new HeroRepository();

        try
        {
            heroRepository.Remove(null);
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(heroRepository.Heroes.Count == 0 && ex.Message == "Name cannot be null (Parameter 'name')");
        }
    }

    [Test]
    public void RemoveAnInvalidHeroWithWhiteSpaceArgument()
    {
        var heroRepository = new HeroRepository();

        try
        {
            heroRepository.Remove("");
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(heroRepository.Heroes.Count == 0 && ex.Message == "Name cannot be null (Parameter 'name')");
        }
    }

    [Test]
    public void GetHeroWithHighestLevelWorksProperly()
    {
        var heroRepository = new HeroRepository();

        var hero = new Hero("a", 1);
        var hero1 = new Hero("b", 2);
        var hero2 = new Hero("c", 3);
        var hero3 = new Hero("d", 4);
        var hero4 = new Hero("e", 5);

        heroRepository.Create(hero);
        heroRepository.Create(hero1);
        heroRepository.Create(hero2);
        heroRepository.Create(hero3);
        heroRepository.Create(hero4);

        Assert.AreEqual(hero4, heroRepository.GetHeroWithHighestLevel());
    }

    [Test]
    public void GetHeroWorksProperly()
    {
        var heroRepository = new HeroRepository();

        var heroName = "a";
        var hero = new Hero(heroName, 1);

        heroRepository.Create(hero);

        Assert.AreEqual(hero, heroRepository.GetHero(heroName));
    }

    [Test]
    public void RemoveHeroWorksProperly()
    {
        var heroRepository = new HeroRepository();

        var heroName = "a";
        var hero = new Hero(heroName, 1);

        heroRepository.Create(hero);

        Assert.AreEqual(true, heroRepository.Remove(heroName));
    }

    [Test]
    public void RemoveHeroWorksProperlyIMDONE()
    {
        var heroRepository = new HeroRepository();

        var heroName = "a";
        var hero = new Hero(heroName, 1);

        heroRepository.Create(hero);

        Assert.AreEqual(false, heroRepository.Remove("gaddaf"));
    }

    [Test]
    public void HeroesPropertWorks()
    {
        var heroRepository = new HeroRepository();

        var heroName = "a";
        var hero = new Hero(heroName, 1);
        var newlIST = new List<Hero>();
        newlIST.Add(hero);
        heroRepository.Create(hero);

        Assert.AreEqual(newlIST, heroRepository.Heroes);
    }

    [Test]
    public void GetHeroWorks()
    {
        var heroRepository = new HeroRepository();

        Assert.AreEqual(null, heroRepository.GetHero("asdasd"));
    }

}