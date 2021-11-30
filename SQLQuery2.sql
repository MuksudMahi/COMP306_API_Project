CREATE DATABASE project;
GO

USE	project;
GO

CREATE TABLE [Restaurant](
	RestaurantId int primary key identity(1001, 1) not null,
	RestaurantName varchar(30) not null,
	RestaurantType varchar (20) not null
);
GO

CREATE TABLE [Food](
	FoodId int primary key identity(1000, 1) not null,
	RestaurantId int not null,
	FoodName varchar(30) not null,
	FoodDescription varchar (30) not null,
	FoodPrice decimal (5,2) not null,
	CONSTRAINT fk_res_id
		FOREIGN KEY (RestaurantId)
		REFERENCES Restaurant(RestaurantId)
		ON DELETE CASCADE
);
GO


INSERT INTO [Restaurant] VALUES ('restaurant1', 'Mexican');
INSERT INTO [Food] VALUES (1001, 'Taco', 'Some Description', 10.00);

select * from [Restaurant];
select * from [Food];

Drop table [Restaurant];
drop table [food]