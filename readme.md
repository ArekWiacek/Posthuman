# Posthuman
Task planner combined with futuristic gamification system

## Features
- Todo List 
  - This is "serious" part of the app - tool for managing tasks you have to do in your everyday life. You can nest tasks inside each other to create complex goals. 
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

- Other
  - Authentication / Authorization
  - Responsive UI
  - Design by Material UI
  - Real time notifications @ SignalR
  - Dark / light mode

### Todo List
![Screenshot1](https://user-images.githubusercontent.com/10595928/157107573-4ac8352e-f748-439a-998d-302beced0a31.png)

### Creating task
![Screenshot2](https://user-images.githubusercontent.com/10595928/157107735-6b7b6294-8b7c-44c0-b57f-77486610f6a7.png)

### Task actions view modes
![Screenshot10](https://user-images.githubusercontent.com/10595928/157114498-839eb5a2-713c-41d8-9cdf-37d78a0d4367.png)
![Screenshot11](https://user-images.githubusercontent.com/10595928/157114510-5ea67fee-b709-4ead-b936-95234acf9a19.png)

### Display options
![Screenshot9](https://user-images.githubusercontent.com/10595928/157114544-1597e70c-599e-4144-a7ef-21ef21b37431.png)

### Avatar view
![Screenshot3](https://user-images.githubusercontent.com/10595928/157107785-fde5e64a-3409-44cc-b095-327cf8ae160e.png)

### Technology card
![Screenshot4](https://user-images.githubusercontent.com/10595928/157107820-5ee850a9-ded8-4578-b591-b44ea4ffb6f1.png)

### Scientist card
![Screenshot5](https://user-images.githubusercontent.com/10595928/157107843-05b347bd-b7f7-48a3-b8ee-efbbeade2867.png)

### Login page
![Screenshot8](https://user-images.githubusercontent.com/10595928/157114835-e5a2e5f1-4277-4f0a-962c-67b8dae62983.png)


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
