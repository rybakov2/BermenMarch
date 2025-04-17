create database Bookstore
GO
use Bookstore
GO

create table Book(
BookId int identity(1,1) not null,
Title varchar(100) not null,
CONSTRAINT PK_Book PRIMARY KEY  (BookId)
)
GO

create table Author(
AuthorId int identity(1,1) not null,
FirstName varchar(100) not null,
LastName varchar(100) not null,
CONSTRAINT PK_Author PRIMARY KEY (AuthorId)
)
GO

create table Book_Author(
BookId int not null,
AuthorId int not null,
CONSTRAINT PK_BookAuthor PRIMARY KEY(BookId, AuthorId),
CONSTRAINT FK_BookAuthor_Book FOREIGN KEY (BookId) REFERENCES Book(BookId),
CONSTRAINT FK_BookAuthor_Author FOREIGN KEY (AuthorId) REFERENCES Author(AuthorId)
)
GO

SET IDENTITY_INSERT Book ON

insert Book(BookId, Title) VALUES(1,'It')
insert Book(BookId, Title) VALUES(2,'The Talisman')
insert Book(BookId, Title) VALUES(3,'Ghost Story')

SET IDENTITY_INSERT Book OFF
GO

SET IDENTITY_INSERT Author ON
insert Author(AuthorId, FirstName, LastName) VALUES(1,'Stephen','King')
insert Author(AuthorId, FirstName, LastName) VALUES(2,'Peter','Straub')
SET IDENTITY_INSERT Author OFF
GO

INSERT Book_Author(BookId, AuthorId) VALUES(1,1)
INSERT Book_Author(BookId, AuthorId) VALUES(2,1)
INSERT Book_Author(BookId, AuthorId) VALUES(2,2)
INSERT Book_Author(BookId, AuthorId) VALUES(3,2)
GO