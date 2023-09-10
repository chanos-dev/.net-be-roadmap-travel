### EntityFramework Tutorial
- PostgreSQL
  - docker-compose.yml
  ```yaml
  version: "3"
  services:
      db:
        image: postgres:latest
        container_name: postgres
        ports:
          - "5431:5432"
        environment:
          POSTGRES_USER: "chanos"
          POSTGRES_PASSWORD: "chanos"
          POSTGRES_DB: "test_db"
        volumes:
          - "${PWD}/volume:/var/lib/postgresql/data"
  ```
- dotnet code-first migration
```
// install dotnet-ef
dotnet tool install --global dotnet-ef --version 6.0.16

// create migration
dotnet ef migrations add init --project .\Migration\EntityFramework.PostgreSQL\EntityFramework.PostgreSQL.csproj

// apply migration
dotnet ef database update --project .\Migration\EntityFramework.PostgreSQL\EntityFramework.PostgreSQL.csproj
```