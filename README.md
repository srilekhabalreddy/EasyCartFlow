# EasyCartFlow
Application has check out flow which is a easy checkout using coupon codes

Used .Net Framework 4.8
Used ASP.NET MVC 5.2.9
Used C# version 7.3

This ASP.NET MVC application showcases a product catalog where users can browse items and add them to their shopping cart. The application utilizes Entity Framework Code First Migrations to manage database schema changes seamlessly.

Steps to Enable Code First Migrations and Update the Database:

**Enable Migrations:**

Open the Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
Execute the command:

``Enable-Migrations``

This command creates a Migrations folder with a Configuration.cs file, enabling Code First Migrations for your project.
Create an Initial Migration:

In the Package Manager Console, run:

``Add-Migration InitialCreate``

This generates a migration script named InitialCreate based on your current data model.
Update the Database:

Apply the migration to your database by executing:

``Update-Database``

This command updates the database schema to match your data model as defined in the migration.
