using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Services;
using DotnetRestApiReference.Infrastructure.InMemory;

namespace DotnetRestApiReference.Tests;

public class UnitTest1
{

    [Fact]
    public void Create_WithInvalidRegionId_ThrowsException()
    {
        // Arrange
        IRegionsRepository regionsRepo = new InMemoryRegionsRepository();
        IBirdsRepository birdsRepo   = new InMemoryBirdsRepository();
        var birdsService = new BirdsService(birdsRepo, regionsRepo);
        var bird = new Bird(1, "Test Bird", "Test Species", new List<int> { 1 });

        // Act
        var exception = Assert.Throws<Exception>(() => birdsService.Create(bird));

        // Assert
        // Note: you may or may not want to check the message;
        // typically only do so if structured messaging is part of the contract
        // Assert.Equal("Invalid region id: 1", exception.Message);

    }

    [Fact]
    public void Create_WithValidData_AssignsIdAndPersists()
    {
        // Arrange
        IRegionsRepository regionsRepo = new InMemoryRegionsRepository();
        regionsRepo.Add(new Region(1, "Test Region"));

        IBirdsRepository birdsRepo   = new InMemoryBirdsRepository();
        var birdsService = new BirdsService(birdsRepo, regionsRepo);
        var bird = new Bird(1, "Test Bird", "Test Species", new List<int> { 1 });

        // Act
        var result = birdsService.Create(bird);

        // Assert
        Assert.True(result.Id > 0);
        Assert.Equal(bird.CommonName, result.CommonName);
        Assert.Equal(bird.Species, result.Species);
        Assert.Equal(bird.RegionIds, result.RegionIds);
    }
}
