# KironTest Web API
Access
--------
GET     /api/Access                - Retrieve a list of all users names & passwords
POST    /api/Access                - Create a new user
GET     /api/Access/{id}           - Retrieve the user with the id number equal to the id entered as parameter
PUT     /api/Access/{id}           - Update the user at the id entered as parameter
DELETE  /api/Access/{id}           - Delete the user at the id number entered as parameter
PUT     /api/Access/{id}           - Acts as "Login" and if successful returns the JWT Token

CoinStats
------------
GET     /api/CoinStats            - Returns the coin stats list (Needs the token generated above...so cannot be executed via Swagger)

Navigation
--------------
GET     /api/Navigation           - Serialize the information in the Navigation table 

UKBankHolidays
---------------------
GET     /api/UKBankHolidays       - Returns the complete list of UK Bank Holidays
