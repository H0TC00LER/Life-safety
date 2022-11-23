create table Comments(
	articleId int,
	userId int,
	text varchar(300),
	date date,
	foreign key (articleId) references Articles(articleId) on update no action,
	foreign key (userId) references Users(userId) on update no action
)