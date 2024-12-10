EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"
drop database mamilots_db;
create database mamilots_db;

create table categories(
	id int primary key identity(1,1),
	name varchar(20) not null
);

create table products(
	id int primary key identity(0,1),
	name varchar(50) not null,
	image varchar(max) default '\Assets\Images\Products\Default.png',
	is_best_seller bit default 0,
	categories_id int not null,
	price smallmoney not null,
	created_at DateTime default Current_Timestamp,
	updated_at DateTime
	constraint FK_categoriesproducts foreign key(categories_id) references categories(id) 
);

create table logs(
	id int primary key identity(0,1),
	total_price money not null,
	created_at DateTime default Current_timestamp,
);

create table transaction_products(
	id int primary key identity(0,1),
	log_id int not null,
	product_id int not null,
	quantity int not null,
	constraint FK_productstransaction_products foreign key (product_id) references products(id),
	constraint FK_logstransaction_products foreign key (log_id) references logs(id)
);