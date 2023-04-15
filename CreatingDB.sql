create database ToDoListDB;

use ToDoListDB;

create table Categories (
	id int identity(1, 1) primary key,
	name varchar(255) not null
);

create table Tasks (
	id int identity(1, 1) primary key,
	category_id int foreign key references Categories(id) not null,
	name varchar(255) not null,
	due_date datetime not null,
	is_completed bit not null
);

insert into Categories values ('Study');
insert into Categories values ('Job');
insert into Categories values ('Family');
insert into Categories values ('Products');
insert into Categories values ('Other');

insert into Tasks values (1, 'Learn SQL Server', '2023-04-12 12:00', 1);
insert into Tasks values (2, 'Create MVC app', '2023-04-13 13:00', 0);
insert into Tasks values (3, 'Help mom', '2023-04-13 13:00', 1);
insert into Tasks values (4, 'Buy products', '2023-04-14 14:00', 0);
insert into Tasks values (5, 'Check website', '2023-04-15 15:00', 0);
insert into Tasks values (1, 'Learn MVC', '2023-04-18 18:00', 0);
insert into Tasks values (1, 'Learn MVC2', '2023-04-21 21:00', 0);
insert into Tasks values (2, 'Do some job', '2023-04-20 20:00', 0);
insert into Tasks values (4, 'Buy coffee', '2023-04-16 16:00', 0);