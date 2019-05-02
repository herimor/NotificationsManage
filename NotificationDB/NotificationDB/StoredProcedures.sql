
-- SelectActualEntries

SELECT
	sc.ID,
	sc.PhoneNumber,
	sc.AppointmentDate,
	sc.Doctor_id,
	d.FirstName,
	d.SecondName,
	d.Patronymic,
	d.Specialization_id,
	sc.SendingStatus
FROM Schedule sc
	LEFT JOIN Doctors d ON sc.Doctor_id = d.ID
WHERE sc.AppointmentDate < @AppointmentDate
	AND sc.SendingStatus = 1
	AND sc.TransactionId IS NULL

-- AddSchedule

INSERT INTO Schedule(
	Doctor_id, 
	PhoneNumber, 
	AppointmentDate, 
	SendingStatus)
VALUES(
	@DoctorId,
	@PhoneNumber,
	@AppointmentDate,
	@SendingStatus)

-- DeleteSchedule

DELETE FROM Schedule 
WHERE ID = @ScheduleID

-- UpdateSchedule

UPDATE Schedule
SET 
	Doctor_id = @DoctorId,
	PhoneNumber = @PhoneNumber,
	AppointmentDate = @AppointmentDate,
	SendingStatus = @SendingStatus,
	TransactionId = @TransactionId
WHERE ID = @ScheduleID

-- PartialScheduleUpdate

UPDATE Schedule
SET 
	Doctor_id = @DoctorId,
	PhoneNumber = @PhoneNumber,
	AppointmentDate = @AppointmentDate
WHERE ID = @ScheduleID

-- AddDoctor

INSERT INTO Doctors(
	FirstName, 
	SecondName, 
	Patronymic, 
	Specialization_id)
VALUES(
	@FirstName,
	@SecondName,
	@Patronymic,
	@SpecializationID)

-- DeleteDoctor

DELETE FROM Doctors WHERE ID = @DoctorID

-- UpdateDoctor

UPDATE Doctors
SET 
	FirstName = @FirstName,
	SecondName = @SecondName,
	Patronymic = @Patronymic,
	Specialization_id = @SpecializationID
WHERE ID = @DoctorID

-- GetAllDoctors

SELECT 
	ID, 
	FirstName, 
	SecondName, 
	Patronymic, 
	Specialization_id 
FROM DOCTORS

-- GetAllSchedule

SELECT
	sc.ID,
	sc.PhoneNumber,
	sc.AppointmentDate,
	sc.Doctor_id,
	d.FirstName,
	d.SecondName,
	d.Patronymic,
	d.Specialization_id,
	sc.SendingStatus,
	sc.TransactionId
FROM Schedule sc
	LEFT JOIN Doctors d ON sc.Doctor_id = d.ID

-- GetScheduleByDoctorId

SELECT
	sc.ID,
	sc.PhoneNumber,
	sc.AppointmentDate,
	sc.Doctor_id,
	d.FirstName,
	d.SecondName,
	d.Patronymic,
	d.Specialization_id,
	sc.SendingStatus,
	sc.TransactionId
FROM Schedule sc
	LEFT JOIN Doctors d ON sc.Doctor_id = d.ID
WHERE d.ID = @DoctorID

-- AddUser

INSERT INTO Users(
	Login, 
	Password) 
VALUES(
	@Login, 
	@Password)

-- GetUserByLogin

SELECT 
	Login, 
	Password 
FROM Users 
WHERE Login = @Login

-- GetTransactedSchedule

SELECT
	sc.ID,
	sc.PhoneNumber,
	sc.AppointmentDate,
	sc.Doctor_id,
	d.FirstName,
	d.SecondName,
	d.Patronymic,
	d.Specialization_id,
	sc.SendingStatus,
	sc.TransactionId
FROM Schedule sc
	LEFT JOIN Doctors d ON sc.Doctor_id = d.ID
WHERE sc.TransactionId IS NOT NULL

-- DeleteObsoleteSchedule

DELETE FROM Schedule 
WHERE AppointmentDate < @AppointmentDate