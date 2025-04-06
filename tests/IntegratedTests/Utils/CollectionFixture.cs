namespace IntegratedTests.Utils;

[CollectionDefinition("IntegratedTests", DisableParallelization = true)]
public class TestCollection : IClassFixture<CustomWebApplicationFactory> { }