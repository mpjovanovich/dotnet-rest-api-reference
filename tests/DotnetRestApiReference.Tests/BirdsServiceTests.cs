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

        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { });
        sut.Create(bird);

        // This has multiple tests / assertions under a common setup.  It could
        // arguably be separate tests, but I prefer the convenience for this
        // small project.

        // duplicate common name
        bird = new Bird(0, "Test Bird", "New Species", new List<int> { });
        Assert.Throws<ConflictException>(() => sut.Create(bird));

        // duplicate species
        bird = new Bird(0, "New Bird", "Test Species", new List<int> { });
        Assert.Throws<ConflictException>(() => sut.Create(bird));
    }

    [Fact]
    public void creating_bird_in_nonexistent_region_is_rejected()
    {
        BirdsService sut = CreateSut();
        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { -1 });

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
        var bird = new Bird(0, "Test Bird", "Test Species", new List<int> { 1 });

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
        var created = sut.Create(new Bird(0, "Test Bird", "Species 1", [1]));

        // Act - update species but leave common name the same
        Bird bird = new Bird(created.Id, "Test Bird", "Species 2", [1]);
        Bird updated = sut.Update(bird);

        // Assert
        Assert.Equal("Test Bird", updated.CommonName);
    }

    /* TODO
    Update — happy path
    Update — bird not found is rejected
    Update — duplicate against a different record is rejected (different from your existing regression test, which is the inverse)
    Delete — happy path
    Delete — not found is rejected
    */
}
