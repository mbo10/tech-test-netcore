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