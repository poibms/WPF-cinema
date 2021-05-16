use CinemaDB

create table Halls 
(
	HallsId int primary key identity(1,1),
	HallsName nvarchar(100)	not null,
	Capacity int not null
)



create table Films 
(
	FilmsId int primary key identity(1,1),
	FilmsName nvarchar(100) not null,
	Country nvarchar(100) not null,
	Director nvarchar(100) not null,
	Genre nvarchar(100) not null,
	Time time not null,
	Description nvarchar(250) not null
)


create table Session 
(
	SessionId int primary key identity(1,1),
	HallsId int foreign key references dbo.Halls(HallsId),
	FilmsId int foreign key references dbo.Films(FilmsId),
	Time time
)

create table Tickets 
(
	TicketsId int primary key identity(1,1),
	SessionId int foreign key references dbo.Session(SessionId),
	Place int not null
)
create table Users
(
	UserId int primary key identity(1,1),
	Name nvarchar(100),
	Login nvarchar(100) not null,
	Password nvarchar(100) not null,
	Role int default 0
)

create table OrderTickets
(
	Id int primary key identity(1,1),
	UserId int foreign key references dbo.Users(UserId),
	TicketsId int foreign key references dbo.Tickets(TicketsId)
)




