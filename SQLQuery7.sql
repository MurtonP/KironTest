CREATE TABLE Users (
    id int IDENTITY(1,1) PRIMARY KEY,
    UserName varchar(255) NOT NULL,
    Password varchar(255) NOT NULL
)