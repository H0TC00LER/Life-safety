create table Friends(
	userId int,
	friendId int,
	foreign key (userId) references Users(userId)
)