create procedure EnumerateCar
as
begin

	select ID, [Name], [Description] 
	from Cars

end