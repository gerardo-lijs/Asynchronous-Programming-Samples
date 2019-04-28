# Scalability and performance in web applications demo
Demo source code of two Web API to demonstrate performance of using async with IO-bound and CPU-bound operations

# Web API running IO-bound operation

## Synchronous code
![](slides/02-AsyncWebAPI-IO-bound-CodeSync.PNG)

## Asynchronous code
![](slides/03-AsyncWebAPI-IO-bound-Code.PNG)

## Performance compare
![](slides/01-AsyncWebAPI-IO-bound-Compare.PNG)

> Using async/await makes it approximately ten times better!

# Web API running IO-bound operation

## Synchronous code
![](slides/05-SyncCode-CPU-bound.PNG)

## Asynchronous code
![](slides/06-AsyncCode-CPU-bound.PNG)

## Performance compare
![](slides/04a-AsyncWebAPI-CPU-bound-Compare.PNG)
Synchronous CPU-bound

![](slides/04b-AsyncWebAPI-CPU-bound-Compare.PNG)
Asynchronous CPU-bound

> Using async/await makes it worse in this case!

## Summary

* Use async with IO-bound operations whenever possible
* Don’t block!
* Don’t use async for expensive CPU-bound operations
* Use benchmarking tools such as Bombardier (https://github.com/codesenberg/bombardier)

I still need more performance, what can I do?
* Scale up
* Scale out whole application using Docker/Kubernetes
* Scale out only expensive CPU-bound operations to Azure Functions
