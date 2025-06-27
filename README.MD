# ToDoList

Simple "todo list" app.

# How to run in dev mode

Requirements:

.Net 8 SDK
Node.js v22.11.0
npm v10.9.0
PostgreSQL db.

1.
```bash
git clone https://github.com/SzymonLegowski/ToDoList.git   
cd ToDoList
```

2. 
Add environment variables  

```bash    
export TODOLIST_DB_CONNECTION="Host=localhost;Port=your-db-port;Database=your-db-name;Username=your-postgres-username;Password=your-postgres-password"    
export APPSETTINGS__KEY="your-key-goes-here"
```  
3.
Prepare postgres database(docker)

```bash
docker pull postgres:latest

docker run \
--name your-db-name \
-e POSTGRES_USER=your-username \
-e POSTGRES_PASSWORD=your-password \
-e POSTGRES_DB=your-db-name \
-p your-db-port:5432 \
postgres:latest
```

4.
Backend  
default port is 5002

```bash
cd todolist-api  
dotnet restore  
dotnet build
dotnet ef migrations add Init
dotnet ef database update
dotnet run
```

5.
Frontend
default port is 5004

```bash
cd todolist-ui
npm install
npm run dev
```

App should be available on http://localhost:5004/