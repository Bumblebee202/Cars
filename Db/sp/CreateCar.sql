USE [cars]
GO
/****** Object:  StoredProcedure [dbo].[CreateCar]    Script Date: 05.10.2020 1:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---- ================================================
---- Template generated from Template Explorer using:
---- Create Procedure (New Menu).SQL
----
---- Use the Specify Values for Template Parameters 
---- command (Ctrl-Shift-M) to fill in the parameter 
---- values below.
----
---- This block of comments will not be included in
---- the definition of the procedure.
ALTER procedure [dbo].[CreateCar]
	@ID nvarchar(max),
	@Name nvarchar(256),
	@Description nvarchar(256)
as
begin

	insert [Cars] (ID, [Name], [Description])
	values (@ID, @Name, @Description)

end