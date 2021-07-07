# BookReselling

Backend part of a book reselling website. 

Key terms:
 * Microservice architecture
 * Gateway Api
 * Jwt authentication
 
 Uses [localstack](https://github.com/localstack/localstack) S3 as file storage.
 
 Consists of:
 * Gateway Api using [Ocelot](https://github.com/ThreeMammals/Ocelot)
 * Identity WebApi for user management and auth, using [Identity Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio)
 * Catalog WebApi
 * (planned) UserProfile WebApi
 * (planned) Chatting Service
 * (planned) Marked products Service
