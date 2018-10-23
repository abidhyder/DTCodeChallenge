# DealerTrackCodeChallenge
Application: This is an application used to upload the data using a CSV file format and then display the uplaoded data on the screen. This CSV file will contain data for Vehicles sold by different dealers. The application will also display the most sold vehicle on the screen.

This application is developed in ASP.NET MVC technology. The Source code has 3 projects
 1. Web.UI - UI related files (Views,Controllers etc)
 2. Web.Business -  layer which has the business logic
 3. UnitTests - Unit tests that will test the business logic
 
 StructureMap is the IOC/DI Container used for injecting dependencies for both the UI and Business layers.
 Repository pattern is used in the business layer.
 Nunit is the testing framework used for developing Unit tests
 NSubstitute is the mocking framework used for mocking the various dependencies to test the system in isolation.
