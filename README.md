# Project setup documentation


this projet is chat communicating with a server on a docker container

docker is necessary for running the project


local project setup : 
  ```
  git clone git@github.com:Okaeri-nasaii/B2ProjetInfra.git
  ```
    
docker setup : 
    
image creation : 
```
cd \ProjetInfraB2\ProjetInfraB2

docker build -t projet .
```

container creation : 
```
cd ..\..

docker-compose up
```
after executing `docker-compose up` command, this sould be display in your terminal :

```
Starting chat_server ... done
Attaching to chat_server
chat_server  | Chat Server Started ....
```

in this state the server can receive message from the client and display them to another client.

you can also change the server port in the server .env file : 
```
PORTSERV=*enter your value here*
```

you can do the same thing for the client port and ip : 

```
IP_ADDRESS=*ip*
PORT=*port*
```
