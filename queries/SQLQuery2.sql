CREATE TABLE Articles(
	articleId int primary key identity(1,1),
	userId int,
	text varchar(3000),
	date date,
	foreign key (userId) REFERENCES Users (userId) on update cascade
	)