[Translated from Spanish to English - www.onlinedoctranslator.com](https://www.onlinedoctranslator.com/en/?utm_source=onlinedoctranslator&utm_medium=pdf&utm_campaign=attribution)![](Aspose.Words.0fd24433-774f-423e-b109-71380a97f9cc.001.png)![](Aspose.Words.0fd24433-774f-423e-b109-71380a97f9cc.002.png)

**Task Scheduler Project** :computer:	

**Overview**

The Task Scheduler Project is a sophisticated task automation system designed to streamline the scheduling and execution of recurring tasks. This project serves as a robust interface for users to input URLs and cron expressions, enabling precise scheduling of tasks such as URL pinging and header scraping at specified intervals.

**Technologies Used**

- Backend: Developed using .NET 8.0, the backend of this project leverages the power and flexibility of the .NET framework to handle task scheduling, data management, and background task execution.
- Database Management: SQL Server, managed through SQL Server 

  Management Studio, is used for efficient and reliable data storage and management.

- Frontend: The user interface is built with Angular 14.2.0, providing a modern and intuitive platform for users to interact with and manage their scheduled tasks.
- Task Automation: The project uses HangFire, a reliable and efficient library for background task processing, ensuring tasks are executed seamlessly and on schedule.
- Unit Testing: Xunit is used for unit testing, ensuring code quality and reliability throughout the development process.

**Features**

- UI Interface: A user-friendly interface allows users to input URLs and cron expressions, facilitating the scheduling of tasks with precision and ease.
- Task Scheduling: The system supports the scheduling of tasks based on cron expressions, enabling users to define specific intervals for task execution.
- Background Task Execution: Tasks are executed efficiently in the background using HangFire, ensuring minimal disruption to the user experience.
- Data Storage: SQL Server managed through SQL Server Management Studio stores task-related data, providing a reliable and secure database solution.

**Installation:
**
● First, you need to download Visual Studio: https://

[visualstudio.microsoft.com/es/vs/community/](https://visualstudio.microsoft.com/es/vs/community/)

● Download Visual Studio Code:https://[code.visualstudio.com/download](https://code.visualstudio.com/download)

● Download Sql Server Management Studio and Sql Server since when using the 

HangFire library, it will need a database: [https://www.microsoft.com/es-co/sql-server/sql-server-downloads https:// ](https://www.microsoft.com/es-co/sql-server/sql-server-downloads)learn.microsoft.com/es-es/sql/ssms/download-sql-server-management-studiossms? view=sql-server-ver16

● Download Git for Windows:https://[git-scm.com/](https://git-scm.com/)

- Install Node.js: Angular uses Node.js and npm (Node Package Manager) to manage its dependencies. If you don't have Node.js installed, download and install it from[nodejs.org. Node.js](https://nodejs.org/) also automatically installs npm.

- Install Angular CLI (Command Line Interface) in version 14: Open your terminal or command line and run the following command to install Angular CLI globally on your system:

`  npm install -g @angular/cli@14
`
- To check the Node.js, npm, and Angular CLI versions on your system, follow 

  these commands: 
`  node --version npm --version
`
`  ng --version or ng version
`
- Then we are going to clone the repository in the folder of your choice, open the terminal (or console) in the location you want to use and execute the command:

`git clone <https://github.com/CamiGCodes/TaskSchedulerTest.git>
`
**************************************************

**USE FOR BACKEND:
**
● Once SQL Server and SSMS are installed, create a database for Hangfire and connect 

to it using the Visual Studio appsettings.json file, for example:

`{

"ConnectionStrings": {

"HangfireConnection": "Data Source=user;Initial Catalog=HangfireTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"

},![](Aspose.Words.0fd24433-774f-423e-b109-71380a97f9cc.003.png)

"Logging": {

"LogLevel": {

"Default": "Information",

"Hangfire": "Information",

"Microsoft.AspNetCore": "Warning"

}

},

"AllowedHosts": "\*"

}
`

- After cloning, we go to the file explorer and go to the path where TaskSchedulerRockWell is:

C:\../TaskSchedulerTest\TaskSchedulerRockWell

- We open the file TaskSchedulerRockWell.sln in Visual Studio
- We configure the execution profile, in the Debug or Debug option, select IIS Express

********************************************

**How to use APIs in .Net backend project?
**

You can run the application and by default the **Swagger** page will open. There you can select the API that you are going to call,

You can also install **Postman**, and consume the APIs from this tool: https:// [www.postman.com/](https://www.postman.com/)

**For the POST API:
**
**Payload:** [https://localhost:44390/api/tasks/scheduleTask ](https://localhost:44390/api/tasks/scheduleTask)

**Body example:**
`{

"chron": {

"minutes":"0", "hours":"23", "dayOfMonth": "eleven", "month":"4", "dayOfWeek": "4"

},

"url":"www.google.com"

}`


**For the GET API: 
**
**Payload:** <https://localhost:44390/api/tasks/getHangFireState>
**No Body required
**

*********************************

**HangFire Usage:
**

With the URL, using the specific port of your machine you can view the Hangfire administration panel:
`https://localhost:portnumber/hangfire
`

This dashboard provides detailed information about scheduled, queued, and completed tasks within the application.

When we access `https://localhost:portnumber/hangfire` in an application that uses Hangfire, the Hangfire administration panel usually appears. This panel provides detailed information about scheduled and queued tasks within the application. Here are some things we can see and do in the Hangfire dashboard:

**Dashboard:** The main dashboard shows a summary of scheduled, queued and completed tasks. Provides metrics such as the number of jobs executed, failed, and waiting.

**Queued Jobs:** We can view the queued jobs, including their current status (pending, running, completed, failed, etc.), estimated waiting time, and other related details.

**Job History: **There is a section that shows the history of completed jobs, including information such as execution duration, result, and start and end time.

**Job Retries:** If a job has failed, we can see the attempts to retry the execution and their result.

**Task Scheduling:** New tasks can be scheduled from the Hangfire dashboard, allowing you to configure recurring or deferred jobs based on a specific schedule.

**Settings and Options: ** Depending on how the Hangfire panel is configured in the app, we can also find options to adjust job settings, view Hangfire server details, and other related settings.

**********************************************************
**To run the UI app:
**
- Open Visual Studio Code as administrator.
- Go to the File -> Open Folder option and go from the repository folder to the ..UIRockwellTaskScheduler\RockwellTaskScheduler folder

C:\..\TaskSchedulerRockWell\TaskSchedulerTest\UIRockwellTaskScheduler\Rockwe llTaskScheduler

- Go to the terminal from this location and proceed to install Project Dependencies:

`  npm install
`
- Once the dependencies are installed, you can serve your Angular application locally on your machine using the following command:

`  ng serve
`
- The url of the server where the application is running will appear. For example: `http://localhost:4200/`
- Then, open the url in your preferred browser.

***************************************************
**To submit data using the form make sure of the following: 
**
**Cron Expression:
**
It should be of the format \* \* \* \* \*, where each \* represents a field for minutes, hours, day of month, month, and day of week respectively. 

Keep in mind that there are 5 values separated by space.

- **Minute:**From 0 to 59.
- **Hour:**From 0 to 23.
- **Day of the month:**From 1 to 31.
- **Month:**From 1 to 12 or by name (for example, JAN, FEB, MAR).
- **Weekday:**From 0 to 6 (0 is Sunday) or by name (for example, SUN, MON, TUE).

**Valid examples:
**
1. * \* \* \* \* (each minute)

2. 0 0 \* \* \* (at midnight every day)

3. * /5 \* \* \* \* (every 5 minutes)

4. 0 12 \* \* MON (at noon every Monday) Example of invalid expression:

5. 1 2 3 4 5 (does not follow the expected format) URL:

- **It must be in the format of a valid URL, starting with http:// or https://. Valid examples:
**
1. "https://www.example.com"

2. "http://localhost:3000"

3. "https://api.servidor.com/ruta"

*******************************************

With Schedule Task button you will schedule the task (ping and scrape the entered url) based on your CRON expression.

With Load Scheduled Tasks button you can see all the tasks with their results that contain the url headers and the state in which they are found.

Keep in mind that for global operation, you must have both the backend and frontend projects running.

Note: important to configure the correct port of your localhost on the backend: 
- In  app.components.ts for the POST
- In hangfire.service.ts for the GET

**************************************************

**The project uses the following technologies:
**
- C#: 56.3%
- TypeScript: 26.7%
- HTML: 8.4%
- CSS: 4.7%
- JavaScript: 3.9%
**
Software Made by:**
:woman_technologist: Maria Camila Chica Gómez. Net Developer and Systems Engineer


