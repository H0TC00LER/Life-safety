CREATE TABLE Users(
	userId int primary key identity(1,1),
	login varchar(20),
	password varchar(20),
	description varchar(3000),
)