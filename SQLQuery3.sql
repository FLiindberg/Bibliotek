select * from Customer
select * from Author
select * from Books
select * from BookAuthors
select * from Inventory
select * from Bookloan
insert into Customer (LoanCard, FirstName, LastName, PhoneNumber)
values (198, 'Filip','Lindberg','0762596359')
insert into Author (FirstName, LastName)
values ('Anders','Sevnsson')
insert into Books (BookTitle, Isbn, ReleaseYear, Rating)
values ('Hej', 19851489, 2020, 3)
insert into BookAuthors (BookId, AuthorId)
values (1, 1)
insert into Inventory (BookId)
values (1)
insert into Bookloan (InventoryId, CustomerId, LoanDate, DueDate)
values (2, 1, '2020-12-12', '2021-01-12')