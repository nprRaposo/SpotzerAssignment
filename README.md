# SpotzerAssignment

This is the resolution for the Exercise propose by Spotzer.
A few things to notice:

The architecture of the application is n-tier based, including the following tiers:
1. Controller Tier : In this tier I include the handling of incoming request from the USER
2. Business Tier: In this tier I include the Business Logic. Phisically is divided into 2 projects (Services & Models) because a restriction of Visual studio regarding circular references.
3. Data Tier: In this tier I include the logic to persist the data. For this purpose I choose the Repository pattern, and I use Entity Framework as a Unit of Work.

I also include some other concepts:

- Dependency Injection: Thru Ninject, I implement the injection of dependencies E.g the Controller receive the Service, and the Service receives the Repositories.
- Test Project: In where I include the most important tests cases I think have to be present on the solution
- Entity Framework: I choose EF as a ORM and the special persist mode in memory, only for this presentation.
- ErrorMessages: I include some Message Format in order to return to the caller, in order to notice him about the result of the operation.
