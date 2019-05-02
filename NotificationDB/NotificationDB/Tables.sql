
CREATE TABLE Doctors(
ID integer primary key autoincrement,
FirstName varchar(50),
SecondName varchar(50),
Patronymic varchar(50),
Specialization_id integer
);

CREATE TABLE Schedule(
ID integer primary key autoincrement,
Doctor_id integer,
PhoneNumber varchar,
AppointmentDate datetime,
SendingStatus integer,
TransactionId integer,
FOREIGN KEY (Doctor_id) REFERENCES Doctors(ID)
);

CREATE TABLE Procedures(
ID integer primary key autoincrement,
ProcedureText varchar(255)
);

CREATE TABLE Users(
ID integer primary key autoincrement,
Login varchar(255),
Password varchar(255)
);