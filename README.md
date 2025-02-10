# EmailService

Email sending service with JWT authentication, failover between email providers, and daily quota control per user.

Main Features:
- Authentication and Authorization: Uses JWT with a 1-hour expiration.
- Failover Between Providers: If one provider fails, the service automatically switches to another.
- Sending Limit: Maximum of 1000 emails per user per day, validated based on database records.
- Statistics Endpoint: Accessible only to administrators, showing the number of emails sent by each user during the day.
- Deployment in Docker: Ready-to-run containerized image.
- Swagger Documentation: Facilitates testing and integration.
- Focus on Performance and Extensibility: Modular code for easy integration of new providers.

## Utilities
1. Runs a container named emailservice-container from the emailservice image, mapping port 8080 of the container to port 8080 on the host machine.
```sh
docker run -p 8080:8080 --name emailservice-container emailservice
```

2. Builds the emailservice image using the Release configuration.
```sh
docker build --build-arg BUILD_CONFIGURATION=Release -t emailservice .
```

3. Admin user:
```sh
{
  "userName": "admin",
  "password": "Admin123!"
}
```

4. Deletes the Migrations folder and all its contents.
```sh
rm -r Migrations/
```

5. Drops the database forcefully, removing all data and schema.
```sh
dotnet ef database drop --force
```

6. Creates a new migration named FullDbChanges, capturing schema changes.
```sh
dotnet ef migrations add FullDbChanges
```

7. Applies the latest migrations to update the database schema.
```sh
dotnet ef database update
```