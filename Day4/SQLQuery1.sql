insert into Person values('11111111111','Ana','Anić','ana@gmail.com');
insert into Person values('12111111111','Petar','Perić','petar@gmail.com');
insert into Person values('11211111111','Jan','Horvat','jan@gmail.com');
insert into Person values('11122111111','Lucija','Mate','lucija@gmail.com');

insert into Dog values (1,'Bobi',2020,'11111111111');
insert into Dog values (2,'Boa',2017,'11111111111');
insert into Dog values (3,'Maza',2009,'12111111111');
insert into Dog values (4,'Roki',2017,'11211111111');
insert into Dog values (5,'Ben',2021,'11122111111');

create index year_ on Dog(bir_year);
