# Clinic-website
A clinical website used to store information about doctors, patients, and medications.

FRONT-END:

    Currently this project just uses the angular-in-memory-web-api npm package to generate fake data for doctors. We could theoretically extend this to all our data types if we wanted.

    ## Development server

    Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

    ## Code scaffolding

    Run `ng generate component component-name` to generate a new component.

    ## Build

    Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

BACK-END:

  ## Setup

  1. [Download the MySQL installer](https://dev.mysql.com/doc/refman/8.0/en/installing.html) and install MySQL Server 8.0.27. Complete the standard installation process, being sure to note the root password for MySQL Server.
  2. [Download MySQL Workbench](https://dev.mysql.com/downloads/workbench/) and install.
  3. Create a directory `C:\Program Files\MySQL\MySQL Server 8.0\data`
  4. Open a command prompt as admin and run `"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqld" --install`
  5. Run `"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqld" --initialize`
  6. In MySQL Workbench, you should now see "Local instance MySQL80" under "MySQL Connections". Click on it and type in your root password.
  7. Click the button labelled "Create a new schema in the connected server".
  8. In the schema creation wizard, name your new schema "emr_schema".
  9. Open the script `initialize_db.sql`.
  10. Hit Ctrl+Shift+Enter to execute the script. This will give you a good starting database.

  ## Connecting to database

  The connection string you enter into appsettings.json should look something like "server=localhost;port=3306;user=root;database=emr_schema;password=<pw>". Do not try to commit this file - it will reveal your root password. Come on. That's cybersecurity 101.
