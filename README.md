# bcgx-checkout-api

I have used .net core web api solution called BcgCodingChallenge to solve this problem. The solution consists of 3 projects. All projects are created in .net 7. 

1)   BcgCodingChallenge  web api project
    It consists of a watchcontroller class withPOST method checkout api. There is OpenAPi/swagger support for running application. It consists of two sub folders. 
    The model’s folder contain simple classes for watch and discount with a association (watch has-a discount)relationship between them. 
    
    Repository folder consists of IWatchRepository and WatchRepository. I’ve added repository dependency injection to encapsulate business logic. These are instantiated along with the controllerclass during runtime.

    WatchHelper class contains metadata lookup logic. I declared a static dictionary so it will be available as soon as the class is referenced. Created PopulateWatchLookup method to populate the lookup dictionary.
2)     Unit test project
    Single unit test class with methods to check if cart is empty, invalid watch in the cart and return appropriate validation message. Ensure multiple discounts are being applied wherever applicable and calculate total cost accurately

3)     Docker compose project
    Docker compose file to build image. This implementation is only working locally for now. 

Commit history can be viewed in 'feature-test' branch of this repository. Later merged onto master

Steps to run this application?
    1)     Pull docker image from https://hub.docker.com/r/chaitanyaj2018/bcg_docker 
    2)     Alternatively Download zip file. This application will needs .net CLR and visual studio on your machine.
    3)     Set Web api project as a start up project. Build & run project
    4)     This should open swagger api in your default browser
    5)     Add input and run
    

Further improvements

There is scope to move the metadata information to database and retrieve it through a stored procedure using ADO.net. Additionally model classes can be converted to entity framework models. Azure app service to enable CICD. Scaling this application in cloud with AWS/Azure. Advance api metrics with Prometheus