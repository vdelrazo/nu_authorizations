# nu_authorizations

### Techonologies
.Net Core application

### Patterns
Followed the repository pattern approach by abstracting data access in separated classes.
Program Logic is isolated from the data which is written in lists. This approach allows us to change the place where the data is stored easily while the information flow process remains the same.

### Information Flow
Operations are introduced in the shell and they are processed one by one:
  1. Which type of transaction are we dealing with (account or transaction)
  2. Deserialize information to treat it as a class.
  3. Validate business rules
  4. Add transaction to backup list to have them as references and to an output list including updates and violations.
After all operations are processed then the program reads the output list content to show it in the console.

### Data Management
All data is stored in global lists.

## How to compile (using docker)
Download the application to a local folder.
Open folder location in a console.
Use docker to compile, make sure you already have an image created and then run the next scripts:
- docker build -t nu_authorizations .
- docker run -i --name nameforapp nu_authorizations
Then, the console will be waiting for the input, which are the transactions to be processed. Please write them all here. 

***Important notes: 
The application expect to have a line break after each transaction entered.
If you do not want to add anymore transactions hit enter to insert a blank line and process the lines.
