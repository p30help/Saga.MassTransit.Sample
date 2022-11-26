# Saga.MassTransit.Sample
This sample project implemented by **.Net6** and I used under tools and libraries on it

- MassTransit (https://github.com/MassTransit/MassTransit)
- EF Core (https://github.com/dotnet/efcore)
- RabbitMQ (https://www.rabbitmq.com/)

Before run this project execute these tasks:
- Create a database in Sql Server and set **ConnectionString** in `Program.cs` file on `SagaMachine` project
- Execute `OrderStateTable-CreateScript.sql` script for creating **OrderStateData** table
- Run `docker-compose up` command in cmd for running **RabbitMq** (for seeing RabbitMq Management panel use `http://localhost:15672` url)

Also in visual studio change start up setting to **Multiple Startup** and set start mode for under projects:
- Order.ApiService
- Stock.ApiService
- SagaMachine