## Task 1 
### Solution
- I forked the repo then cloned the code to my local machine.
- I built the solution and ran the tests.
- I applied the existing ef migrations so I can register an account.
- I ran the application, registered two accounts and played around, creating lists and getting used to the functionality of the app.

## Task 2
### Solution
- Inside the foreach on the **Detail.cshtml** page, I used the LINQ **OrderBy** method on the Importance property of Items, which orders the items in an ascending order by default.
- The importance property is an enum so each value is assigned an integer starting from 0 at the first by default.
- Noticed that there is a case missing for Medium importance which leaves it with no colour. I introduced the contextual class **"list-group-item-warning"**, which colours the medium importance tasks in yellow, which improves the app's intuativeness. 

## Task 3
### Solution
- Fixed a flaw in the **TodoItemEditFiels.cs** file, where the class constructor was hardcoding the importance value as a Medium rather than using the value from the **importance** parameter.
- Fixed a flaw in the **TodoItemCreateFiels.cs** file, where the Importance property was being set to Medium by default.
- These flaws were causing one of the tests to fail and also causing a bug in the UI of the app where if you wanted to edit a task, the importance shown would always be Medium.

## Task 4
### Solution
- Added a name data annotation on the ResponsiblePartyId property in both **TodoItemEditFiels.cs & TodoItemCreateFiels.cs** in order to display a friendlier text on the UI.

## Task 5
### Solution
- Added a checkbox on the Detail page, for hiding and unhiding tasks that are marked as done.
- Added a task status variable which checks if an item is marked as done and sets its value to "done" or "in-progress".
- Added an attribute to the list of items, which allows me to filter the items by their status.
- Made use of simple javascript event listeners to handle the actual hiding and unhiding of items 

## Task 6
### Solution
- Introduced the functionality to allow a user to see any to-do lists in which they have at least a single task assigned to them.
- Added an additional condition to the filter, which is responsible for loading the relevant to-do lists for a user, in **ApplicationDbContextConvenience.cs**.
- The condition checks if the responsible party id for any to-do item is equal to the user id of the owner of the current to-do list.

## Task 7
### Solution
#### *Part - 1*
- Added a Rank property of type integer to the TodoItem class.
- Built the solution and solved the expected build failures by introducing the new property to the relevant models and model mappers.
- Fixed the now failing tests
- Introduced an additional test to test the creation of a to-do item with a rank
- Installed EntityFramework Core Tools NuGet package
- Added a new database migration and updated the database
#### *Part - 2*
- Introduced additional input fields in the Create and Edit UI pages to allow for the setting of rank for each item in a list.
- Introduced a boolean property which indicates wether a list is sorted by rank or not and used it to drive the logic behind sorting the lists.
- Displayed the option to sort by Importance or Rank via radiobuttons on the UI

## Task 8
### Solution
- Extended the Gravatar service by making it non-static and introducing a Gravatar Interface to enable me to use the service as a Singleton and inject it where needed.
- Added a new method to the Gravatar service, called **GetInfoAsync**. It performs an API call(GET) to the Gravatar endpoint, which responds with the current user details. I then return said details as an object, which I created, called **ProfileInfo**.
- Introduced another method to the Gravatar service, called **GenerateUserHtmlAsync** which makes use of the **GetInfoAsync** method and returns html elements.
- Refactored the Login and Detail pages to use the **GenerateUserHtmlAsync** method for getting user information.
- Added an extra check on the Detail page to check if the Assignee's email equals that of the current user, which prevents a bug in To-Do lists where I'm not the owner, but only an Assignee.

## Task 9
### Solution
- Refactored the UI so that the creation of new To-do items happens on the Details page instead of being redirected to an isolated page just for the purpose of creation of a new item.
- I extracted a partial view based on the existing Create page and named it after the location at which it will be rendered.(DetailPartial)
- I added a button on the Detail page, which upon being clicked, reveals a form for the creation of a new item on the current list. Upon being clicked the Creation button gets disabled until the form is saved. This is to prevent accidental clicks on it, while an item is in-process of being created.
- I've added a JavaScript event listener method on the Creation button, responding to a click, by performing an HTTP GET on the **GetDetailPartialView** method in the TodoItemController.
- I've introduced a JavaScript function **addFormSubmitHandler()**, which gets triggered from within the create button event listener, that sends an HTTP POST request to the Create() method of the TodoItemController, which then creates and saves the new item.
#### TO-DO
- The scripts section at the bottom of the **Detail.cshtml** has grown along with the progress of the tasks and it would be beneficial for it to be extracted in a separate **.js** file.