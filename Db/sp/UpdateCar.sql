create procedure UpdateCar
	@ID nvarchar(max),
	@Name nvarchar(256),
	@Description nvarchar(256)
as
begin

	update [Cars]
	set
		[Name] = @Name,
		[Description] = @Description
	where ID = @ID;
	
end 