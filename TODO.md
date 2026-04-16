# TODO

Write just a few tests after for this branch to reinforce ideas and that testing is important (see DI convo in Cursor)

Copy commented data from services / repositories to tests, and remove comments to clean up.

In .NET tests go in their own project in order to keep dependencies isolated; there is no equivalent to "development dependencies" like there in npm, e.g.

```
dotnet-rest-api-reference/
  src/
    DotnetRestApiReference.Api/
    ...
  tests/
    DotnetRestApiReference.Tests/
        Services/BirdsServiceTests.cs
        Endpoints/BirdsEndpointTests.cs
  DotnetRestApiReference.sln            (or .slnx)
```
