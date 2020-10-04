create procedure DeleteCar
	@ID nvarchar(max)
as
begin

	delete [Cars]
	where ID = @ID;

end