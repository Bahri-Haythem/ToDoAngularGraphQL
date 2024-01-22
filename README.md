# ToDo with Angular and GraphQL
This is a simple Todo application using:
- Dotnet for backend
- Angular for frontend
- SQL Server as the database

The app uses GraphQL for the API:
- HotChocolate for the server
- Apollo for the client

![todo app image](/ToDo.PNG)

To start the app you can use either compose or kubernetes.

## Docker compose
To run the app, on project root execute the following command:
```docker compose up```

To stop and remove containers:
```docker compose down```

## Kubernetes
You can run the app on kubernetes. Yaml files were generated using the **kompose** conversion tool .I used **minikube** as my kubernetes local cluster. To start it type:
```minikube start```

Then deploy the app with the command (*execute on project root*):
```kubectl apply -f .\k8s.yaml```

Load the local images in minikube (You must build the images with Docker beforehand)
```minikube image load todoangulargraphqlserver```
```minikube image load todoangulargraphqlclient```

To verify:
```minikube image ls```

Finally, you can create a route for services with *tunnel* and expose the frontend service: 
```minikube tunnel```
```minikube service todoangulargraphql-client -n todo```

### For clean up
To delete kubernetes ressources:
```kubectl delete -f .\k8s.yaml```
to stop minikube:
```minikube stop```