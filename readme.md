# Posthuman
Task planner combined with futuristic gamification system

## Features
- Todo List 
  - This is "serious" part of app - tool for managing tasks you have to do in your everyday life. You can nest tasks inside each other to create complex goals. 
  - Creating, editing and managing tasks
  - Nesting tasks to create complex goals
  - Different views - tree, flat, day-by-day
  - Actions menu in two view modes
  
- Avatar
  - This is gamification part of app - simulates developing RPG character, but here you gain experience by completing real life tasks not by killing monsters :)
  - Represents user 'hero'
  - You gain XP points for for completing tasks. With XP your Avatar reaches new levels
  - Then you discover Technology Cards with description of futuristic technologies
  - And Team cards with famous scientists info 

## Technical information

Below you will find technical information about this project - used architecture, design patterns, libraries and technologies. This is useful mostly for developers.

### Architecture

Backend solution is divided into different layers to separate responsibility of different parts of app. Layers are : 
- Core (models)
- Data (here is entity framework implementation)
- Services ( containing "game" logic) 
- Web API (endpoint for outer world to communicate with frontend). 
- Additionally there is additional layer implementing SignalR library to communicate with frontend in real time - called "RealTimeCommunication". 

Frontend application is written in React with MaterialUI.

Below you have more details about each layer

### Posthuman.Core

* Model Layer
* Models definitions
* Entities, DTOs, Enums
* Repositories interfaces 
* Services interfaces
* Unit of work interface

### Posthuman.Data
* Data Layer
* Data context
* Data tables configuration
* Migrations
* Entity Framework integration
* Repositories implementation
* Unit of work implementation

### Posthuman.Service
* Service Layer
* Contains application / game logic
* Implementation of services encapsulating all game logic
* Implementation of game logic helpers, for example randomization system

### Posthuman.WebApi
* Web API Layer
* ASP.NET Core Web API integration
* Web Api Controllers implementation
* Mappings done by Auto Mapper
* Logging implementation by log4net
* Environment type handling

## Technologies used

### Backend
- C#
- ASP.NET Core 3.1
- ASP.NET WebAPI
- Entity Framework
- SignalR
- AutoMapper
- Microsoft SQL Server 2018
- MS Visual Studio 2022

### Frontend
- React
- JavaScript
- MaterialUI
- MS Visual Studio Code
