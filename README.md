## Dotnet microservices app

### Run with
`cd docker-compose  && docker-compose up`

### Add Migration
`dotnet ef migrations add InitialCreate --startup-project ./src/services/Ordering/Ordering.API/ --project ./src/services/Ordering/Ordering.Infrastructure/ -o Data/Migrations`