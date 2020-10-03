create procedure GetCarById
	@ID nvarchar(256)
as
begin

	select ID, [Name], [Description]
	from Cars
	where ID = @ID

end