# Pierre's Bakery - Sweet and Savory Edition

#### By: _Lindsay Warr_

## Description
Pierre's Bakery is a web app that users ASP.NET Identity to login and authenticate users. When logged in, certain user types (admins) have authorization to Create, Delete, and Update flavors and treats. Regular guests and those who are not logged in are able to Read only.

## Technologies Used
- C#
- .NET6 SDK
- ASP.NET Core MVC
- ASP.NET Identity
- EF Core
- SQL
- HTML
- CSS
- Markdown
- Razor

## Setup
_Note: You will need to have the following installed locally before you can run this application:_
- _.NET6_
- _MySQL_
- _MySQL Workbench_
- _VS Code_ 

1. In the terminal run these commands in order:
- `$ git clone https://github.com/user/Pierres.Solution.git`
- `cd Pierres.Solution` 
- `$ touch .gitignore` 
2. copy/paste this into the .gitignore file:
- obj
- bin
- appsettings.json
- .DS_Store (if on a Mac)
3. Navigate to this project's production directory called "SweetAndSavory" with `$ cd SweetAndSavory`.
4. Within the production directory run the command `$ touch appsettings.json`.
5. In the appsettings.json file, paste in the following code, replacing [user-id] and [password] with your  username and password for MySQL Workbench. (Remember to remove the square brackets when inputting your details):
`{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=pierres_solution;uid=[user-id];pwd=[password];"
  }
}`
6. Within the production directory "SweetAndSavory", run `$ dotnet ef database update` to instantiate the database.
7. Still within the production directory, run `$ dotnet watch run` in the command line to launch the application in development mode in a browser, and interact with the application.

## Known Bugs
- Occasionally after adding a treat or a flavor, clicking on edit or delete will route to a 404 error. It's corrected after backing out to the main details page and waiting a few seconds

## Future Considerations
- Better navigation from page to page
- Adding a "cancel" option when on the edit or delete page 

## MIT License
Copyright (c) August 2023 Lindsay Warr