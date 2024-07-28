using FluentValidation.TestHelper;
using JetBrains.Annotations;
using Restaurants.Application.Bussiness;

namespace Restaurants.Tests.Application.Restaurants.Commands.CreateRestaurant;

[TestSubject(typeof(CreateRestaurantCommandValidator))]
public class CreateRestaurantCommandValidatorTest
{
    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var createRestaurantCommand = new CreateRestaurantCommand
        {
            Name = "Test restautant",
            City = null,
            Street = null,
            PostalCode = "69-000",
            ContactEmail = "test@restaurant.com",
            Description = "Ceci est un test de restaurant",
            Category = "Test",
            HasDelivery = false
        };
        var validator = new CreateRestaurantCommandValidator();
        // Act
        var result = validator.TestValidate(createRestaurantCommand);
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ForValidCommand_ShouldHaveValidationErrors()
    {
        // Arrange
        var createRestaurantCommand = new CreateRestaurantCommand
        {
            Name = "Te",
            City = null,
            Street = null,
            PostalCode = "69000",
            ContactEmail = "test@restaurant.com",
            Description = "Ceci est un test de restaurant",
            Category = null,
            HasDelivery = false
        };
        var validator = new CreateRestaurantCommandValidator();
        // Act
        var result = validator.TestValidate(createRestaurantCommand);
        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        result.ShouldNotHaveValidationErrorFor(c => c.ContactEmail);
    }
}