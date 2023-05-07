using NetArchTest.Rules;
using Xunit;

namespace Architecture.Tests.Tests;

/// <summary>
/// Tests for dependency between projects
/// </summary>
public class ArchitectureTests
{
    private const string DomainNamespace = "CarRental.Domain";
    private const string ApplicationNamespace = "CarRental.Application";
    private const string InfrastructureNamespace = "CarRental.Infrastructure";
    private const string PresentationNamespace = "CarRental.Presentation";
    private const string WebApiNamespace = "CarRental.WebApi";

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
    {
        //Arrange
        var assembly = typeof(CarRental.Domain.AssemblyReference).Assembly;
        
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace
        };
        
        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
    
    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Other_Projects()
    {
        //Arrange
        var assembly = typeof(CarRental.Application.AssemblyReference).Assembly;
        
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            WebApiNamespace
        };
        
        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
    
    [Fact]
    public void Infrastructure_Should_Not_Have_Dependency_On_Other_Projects()
    {
        //Arrange
        var assembly = typeof(CarRental.Infrastructure.AssemblyReference).Assembly;
        
        var otherProjects = new[]
        {
            PresentationNamespace,
            WebApiNamespace
        };
        
        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
    
    [Fact]
    public void Presentation_Should_Not_Have_Dependency_On_Other_Projects()
    {
        //Arrange
        var assembly = typeof(CarRental.Presentation.AssemblyReference).Assembly;
        
        var otherProjects = new[]
        {
            WebApiNamespace
        };
        
        //Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Handlers_Should_Have_Dependency_On_Domain()
    {
        //Arrange
        var assembly = typeof(CarRental.Application.AssemblyReference).Assembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
    
    [Fact]
    public void Controllers_Should_Have_Dependency_On_MediatR()
    {
        //Arrange
        var assembly = typeof(CarRental.Presentation.AssemblyReference).Assembly;

        //Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
}