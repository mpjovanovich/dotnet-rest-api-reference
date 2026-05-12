using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Services;
using DotnetRestApiReference.Infrastructure.InMemory;

namespace DotnetRestApiReference.Tests;

public class BirdsServiceTests
{

    private static BirdsService CreateSut()
    {
        // This method sets up in-memory fakes for the tests to use
        IRegionsRepository regions = new InMemoryRegionsRepository();
        IBirdsRepository birds = new InMemoryBirdsRepository(regions);
        regions.Add(new Region(0, "Test Region")); // will have id=1
        return new BirdsService(birds);
    }

    [Fact]
    public void creating_bird_with_duplicate_unique_field_throws()
    {
        // Arrange
        BirdsService sut = CreateSut();

        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { }, "test.jpg");
        sut.Create(bird);

        // This has multiple tests / assertions under a common setup.  It could
        // arguably be separate tests, but I prefer the convenience for this
        // small project.

        // duplicate common name
        bird = new Bird(0, "Test Bird", "New Species", new List<int> { }, "test.jpg");
        Assert.Throws<ConflictException>(() => sut.Create(bird));

        // duplicate species
        bird = new Bird(0, "New Bird", "Test Species", new List<int> { }, "test.jpg");
        Assert.Throws<ConflictException>(() => sut.Create(bird));
    }

    [Fact]
    public void creating_bird_in_nonexistent_region_is_rejected()
    {
        BirdsService sut = CreateSut();
        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { -1 }, "test.jpg");

        // Act / Assert
        Assert.Throws<NotFoundException>(() => sut.Create(bird));

        // Assert
        // Note: you may or may not want to check the message;
        // typically only do so if structured messaging is part of the contract
        // Assert.Equal("Invalid region id: 1", exception.Message);
    }

    [Fact]
    public void creating_bird_with_valid_data_assigns_id_and_persists()
    {
        // Arrange
        BirdsService sut = CreateSut();
        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { 1 }, "test.jpg");

        // Act
        var result = sut.Create(bird);

        // Assert
        Assert.True(result.Id > 0);
        Assert.Equal(result, bird with { Id = result.Id });
    }

    [Fact]
    public void changing_unique_field_does_not_falsely_trigger_unique_error_for_other_unique_field()
    {
        // Arrange
        BirdsService sut = CreateSut();
        var created = sut.Create(new Bird(0, "Test Bird", "Species 1", [1], "test.jpg"));

        // Act - update species but leave common name the same
        Bird bird = new Bird(created.Id, "Test Bird", "Species 2", [], "test2.jpg");
        Bird updated = sut.Update(bird);

        // Assert
        Assert.Equal("Test Bird", updated.CommonName);
    }

    [Fact]
    public void updating_bird_with_valid_data_persists_changes()
    {
        // Arrange
        BirdsService sut = CreateSut();
        Bird created = sut.Create(new Bird(0, "Bird 1", "Species 1", [1], "test.jpg"));
        Bird update = new Bird(created.Id, "Bird 1 Updated", "Species 1 Updated", [1], "test.jpg");

        // Act
        Bird result = sut.Update(update);

        // Assert
        Assert.Equal(update, result);
        Assert.Equal(update, sut.GetById(created.Id));
    }

    [Fact]
    public void updating_nonexistent_bird_is_rejected()
    {
        // Arrange
        BirdsService sut = CreateSut();
        Bird bird = new Bird(-1, "Bird 1", "Species 1", [1], "test.jpg");

        // Act / Assert
        Assert.Throws<NotFoundException>(() => sut.Update(bird));
    }

    [Fact]
    public void updating_bird_with_duplicate_unique_field_on_different_record_is_rejected()
    {
        // Arrange
        BirdsService sut = CreateSut();
        Bird first = sut.Create(new Bird(0, "Bird 1", "Species 1", [1], "test.jpg"));
        Bird second = sut.Create(new Bird(0, "Bird 2", "Species 2", [1], "test2.jpg"));

        // Act / Assert - duplicate common name
        Bird duplicateCommonName = new Bird(second.Id, first.CommonName, second.Species, [1], "test.jpg");
        Assert.Throws<ConflictException>(() => sut.Update(duplicateCommonName));

        // Act / Assert - duplicate species
        Bird duplicateSpecies = new Bird(second.Id, second.CommonName, first.Species, second.RegionIds, second.ImageUrl);
        Assert.Throws<ConflictException>(() => sut.Update(duplicateSpecies));
    }

    [Fact]
    public void deleting_existing_bird_removes_it_and_returns_deleted_bird()
    {
        // Arrange
        BirdsService sut = CreateSut();
        Bird created = sut.Create(new Bird(0, "Bird 1", "Species 1", [1], "test.jpg"));

        // Act
        Bird deleted = sut.Delete(created.Id);

        // Assert
        Assert.Equal(created, deleted);
        Assert.Null(sut.GetById(created.Id));
    }

    [Fact]
    public void deleting_nonexistent_bird_is_rejected()
    {
        // Arrange
        BirdsService sut = CreateSut();

        // Act / Assert
        Assert.Throws<NotFoundException>(() => sut.Delete(-1));
    }
}
