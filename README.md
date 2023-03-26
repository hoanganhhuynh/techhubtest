About the : The structure of the project is splitted to 3 layers. API, Domain and Infrastructure. The idea is got from DDD.
    + TodoAPI: Project includes api
    + Domain: contains entities, contract/interface, exception ..ect
    + Infrastructure: contains dbcontext, the implementation of contracts/interface which are defined in domain.

Precondition
    Install SQLServer in local machine
How to test
    1. Update connection string in appsetting into connection string to connect to expected database
    2. Run 'dotnet ef migrations add InitDatabase  -s TodoAPI -p Infrastructure'
    3. Use Postman or any tool to test API and call to the endpoints to test
        + In order to authenticate with the endpoint. Add 'x-api-key' with value is '2wDf5xsR7F' to header of request